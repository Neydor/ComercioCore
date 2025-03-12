import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component } from '@angular/core';
import {
  Comerciante,
  ComercianteFilterDto,
  ComercianteUpdateDto,
  ComercianteUpdateStatusDto
} from '@core/models/comerciante.model';
import { AuthService } from '@core/services/auth.service';
import { ComerciantesService } from '@core/services/comerciantes.service';
import { ReportesService } from '@core/services/reportes.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { Subscription } from 'rxjs';
import { PaginatorModule } from 'primeng/paginator';
import { SelectModule } from 'primeng/select';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { Router } from '@angular/router';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  imports: [
    CommonModule,
    TableModule,
    TagModule,
    ButtonModule,
    ToastModule,
    PaginatorModule,
    SelectModule,
    IconFieldModule,
    InputIconModule,
    FormsModule,
    DropdownModule,
    DialogModule,
    ConfirmDialogModule,
    ProgressSpinnerModule
  ],
  selector: 'app-comerciantes-list',
  templateUrl: './comerciantes-list.component.html',
  styleUrls: ['./comerciantes-list.component.css'],
  providers: [ConfirmationService, MessageService]
})
export class ComerciantesListComponent {
  comerciantes: Comerciante[] = [];
  first = 0;
  rows: number = 10;
  totalRecords: number = 0;
  currentPage = 1;
  pageSizeOptions = [5, 10, 15];
  pageNumbers: number[] = [];
  totalPages: number = 0;

  selectedPageSize = 10;
  isAdmin = false;
  fechaRegistro: Date | null = null;
  nombreSearch: string = '';
  subscriptions: Subscription[] = [];

  loading: boolean = true;
  constructor(
    private comerciantesService: ComerciantesService,
    private reportesService: ReportesService,
    private confirmationService: ConfirmationService,
    private authService: AuthService,
    private messageService: MessageService,
    private cdr: ChangeDetectorRef,
        private router: Router,

  ) {
    this.isAdmin = this.authService.isAdmin();
  }
  ngOnInit() {
    this.loadComerciantes(1);
  }
  ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  loadComerciantes(page: number = 1) {
    this.currentPage = page;

    const filter: ComercianteFilterDto = {
      page: page,
      pageSize: this.selectedPageSize,
      estado: '',
      fechaRegistro: this.fechaRegistro,
      nombre: this.nombreSearch
    };

    this.subscriptions.push(
      this.comerciantesService.getComerciantes(filter)
        .subscribe({
          next: (response) => {
            if (response.success && response.data) {
              this.comerciantes = response.data.data;
              this.totalRecords = response.data.totalCount;
              this.totalPages = response.data.totalPages;
              this.calcularPaginacion();
              this.loading = false;
              this.cdr.detectChanges();
            } else {
              console.error('Error en la respuesta:', response.message);
            }
          },
          error: (error) => {
            console.error('Error al cargar comerciantes:', error);
          }
        })
    );
  }

  onDelete(id: string) {
    this.confirmationService.confirm({
      message: '¿Está seguro de eliminar este comerciante?',
      acceptLabel: 'Sí',
      rejectLabel: 'No',
      acceptButtonStyleClass: 'p-button-danger',
      rejectButtonStyleClass: 'p-button-secondary',
      accept: () => this.deleteComerciante(id)
    });
  }
  deleteComerciante(id: string) {
    this.comerciantesService.deleteComerciante(id).subscribe(() => {
      this.loadComerciantes();
    });
  }
  exportToCSV() {
    this.reportesService.exportarComerciantesCSV().subscribe(csvData => {
      const blob = new Blob([csvData], { type: 'text/csv;charset=utf-8' });

      const url = window.URL.createObjectURL(blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = url;
      downloadLink.setAttribute('download', 'comerciantes.csv');

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(url);
    });
  }

  updateEstado(id: string, estado: string) {
    const updateStatusDto: ComercianteUpdateStatusDto = { estado: estado === 'Activo' ? 'Inactivo' : 'Activo' };
    this.comerciantesService.updateEstado(id, updateStatusDto).subscribe({
      next: () => {
        this.loadComerciantes();
        this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Estado del comerciante actualizado correctamente' });
      },
      error: (error) => {
        console.error('Error al actualizar estado del comerciante:', error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo actualizar el estado del comerciante' });
      }
    });
  }

  onPageChange(event: any): void {
    this.first = event.first;
    this.rows = event.rows;
    this.currentPage = Math.floor(this.first / this.rows) + 1;
    this.loadComerciantes(this.currentPage);
  }


  crearFormularioNuevo() {
    this.router.navigate(['/comerciantes-form']);
  }
  editarComerciante(comerciante: Comerciante) {
    this.router.navigate(['/comerciantes-form', comerciante.id]);
  }

  onPageSizeChange(event: any) {
    this.selectedPageSize = event.value;
    this.currentPage = 1;
    this.loadComerciantes(1);
  }

  calcularPaginacion() {
    this.totalPages = Math.ceil(this.totalRecords / this.selectedPageSize);
  }
}
