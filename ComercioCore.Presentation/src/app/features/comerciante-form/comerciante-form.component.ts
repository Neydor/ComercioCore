// comerciante-form.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MunicipiosService } from '@core/services/municipios.service';

@Component({
  selector: 'app-comerciante-form',
  templateUrl: './comerciante-form.component.html'
})
export class ComercianteFormComponent implements OnInit {
  form = this.fb.group({
    nombre: ['', [Validators.required, Validators.maxLength(100)]],
    municipioId: [null, Validators.required],
    telefono: ['', Validators.pattern(/^[0-9]*$/)],
    email: ['', Validators.email],
    fechaRegistro: [new Date(), Validators.required],
    estado: ['Activo', Validators.required],
    tieneEstablecimientos: [false]
  });

  municipios: Municipio[] = [];
  estados = ['Activo', 'Inactivo'];

  constructor(
    private fb: FormBuilder,
    private municipiosService: MunicipiosService
  ) {}

  ngOnInit() {
    this.municipiosService.getMunicipios().subscribe(data => {
      this.municipios = data;
    });
  }

  onSubmit() {
    if (this.form.valid) {
      // Lógica para guardar
    }
  }
}
