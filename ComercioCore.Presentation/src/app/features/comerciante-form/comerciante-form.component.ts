// comerciante-form.component.ts
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ComerciantesService } from '@core/services/comerciantes.service'; // Import the service
import { ActivatedRoute, Router } from '@angular/router'; // Import for route handling
import { MunicipiosService } from '@core/services/municipios.service';
import { Municipio } from '@core/models/municipio.model';
import { DropdownModule } from 'primeng/dropdown';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { CalendarModule } from 'primeng/calendar';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { HeaderComponent } from '@layout/header/header.component';
import { SplitterModule } from 'primeng/splitter';
import { DividerModule } from 'primeng/divider';
import { FloatLabelModule } from 'primeng/floatlabel';
import { DatePickerModule } from 'primeng/datepicker';
import { InputTextModule } from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { Comerciante } from '@core/models/comerciante.model';
import { MessageService } from 'primeng/api';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AuthService } from '@core/services/auth.service';
import { SelectModule } from 'primeng/select';
@Component({
  selector: 'app-comerciante-form',
  templateUrl: './comerciante-form.component.html',
  styleUrls: ['./comerciante-form.component.scss'],
  imports: [
    CommonModule,
    HeaderComponent,
    ReactiveFormsModule,
    DropdownModule,
    CardModule,
    CalendarModule,
    CheckboxModule,
    ButtonModule,
    DividerModule,
    SplitterModule,
    FloatLabelModule,
    DatePickerModule,
    InputTextModule,
    InputMaskModule,
    ProgressSpinnerModule,
    SelectModule
  ],
  providers: [MessageService]
})
export class ComercianteFormComponent implements OnInit {
  form: FormGroup;
  municipios: Municipio[] = [];
  estados = ['Activo', 'Inactivo'];
  comercianteId: string | null = null;
  loading: boolean = false;
  comerciante: Comerciante | null = null;
  totalIngresos: number = 0;
  totalEmpleados: number = 0;
  isAdmin: boolean;
  constructor(
    private fb: FormBuilder,
    private municipiosService: MunicipiosService,
    private comerciantesService: ComerciantesService,
    private route: ActivatedRoute,
    private router: Router,
    private messageService: MessageService,
    private authService: AuthService,
        private cdr: ChangeDetectorRef,


  ) {
    this.isAdmin = this.authService.isAdmin();
    this.form = this.fb.group({
      razonSocial: ['', Validators.required],
      email: ['', [Validators.email]],
      city: ['', Validators.required],
      phone: [''],
      registrationDate: ['', Validators.required],
      hasEstablishments: [false]
    });
  }

  ngOnInit() {
    this.municipiosService.getMunicipios().subscribe((data:any) => {
      console.log("muni",data);
      this.municipios = data.data;
    });

    this.route.paramMap.subscribe(params => {
      console.log("aaaaa", params)
      const id = params.get('id');
      if (id) {
        this.comercianteId = id;
        this.loadComerciante(this.comercianteId);
      }
    });
  }

  loadComerciante(id: string) {
    this.loading = true;
    this.comerciantesService.getComercianteById(id).subscribe({
      next: (data: any) => {
        const comerciante = data.data;
        this.comerciante = comerciante;

        this.totalIngresos = comerciante.establecimientos?.reduce((acc: number, e: any) => acc + e.ingresos, 0) || 0;
        this.totalEmpleados = comerciante.establecimientos?.reduce((acc: number, e: any) => acc + e.numeroEmpleados, 0) || 0;
        this.form.patchValue({
          razonSocial: comerciante.nombreRazonSocial,
          email: comerciante.correoElectronico,
          city: {id: comerciante.municipioId, nombre: comerciante.municipioNombre},
          phone: comerciante.telefono,
          registrationDate: new Date(comerciante.fechaRegistro),
          hasEstablishments: comerciante.establecimientos?.length > 0
        });

        this.loading = false;
        this.cdr.detectChanges();

      },
      error: (error) => {
        console.error('Error loading comerciante:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'No se pudo cargar la información del comerciante'
        });
        this.loading = false;
      }
    });
  }
  onSubmit() {
    if (this.form.valid) {
      this.loading = true;
      const formValues = this.form.value;
      const comerciante: Comerciante = {
        id: this.comercianteId || '',
        nombreRazonSocial: formValues.razonSocial,
        correoElectronico: formValues.email,
        municipioId: formValues.city.id,
        telefono: formValues.phone,
        fechaRegistro: new Date(formValues.registrationDate),
        cantidadEstablecimientos: formValues.hasEstablishments ? 1 : 0,
        estado: 'Activo',
        poseeEstablecimientos: formValues.hasEstablishments,
        establecimientos: [],
        municipio: formValues.city
      };
      if (this.comercianteId) {
        this.comerciantesService.updateComerciante(this.comercianteId, comerciante).subscribe({
          next: () => {
            this.loading = false;
            this.messageService.add({
              severity: 'success',
              summary: 'Éxito',
              detail: 'El comerciante ha sido actualizado correctamente'
            });
            setTimeout(() => this.router.navigate(['/comerciantes']), 5000);
          },
          error: (error) => {
            console.error('Error al actualizar comerciante:', error);
            this.loading = false;
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'No se pudo actualizar el comerciante. Por favor, inténtelo de nuevo.'
            });
          }
        });
      } else {
        this.comerciantesService.createComerciante(comerciante).subscribe({
          next: () => {
            this.loading = false;
            this.messageService.add({
              severity: 'success',
              summary: 'Éxito',
              detail: 'El comerciante ha sido creado correctamente'
            });
            setTimeout(() => this.router.navigate(['/comerciantes']), 5000);
          },
          error: (error) => {
            console.error('Error al crear comerciante:', error);
            this.loading = false;
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'No se pudo crear el comerciante. Por favor, inténtelo de nuevo.'
            });
          }
        });
      }
    }
  }
}
