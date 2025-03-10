// comerciantes-list.component.ts
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
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
import { TagModule } from 'primeng/tag'; // Importa el TagModule
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { Subscription } from 'rxjs';
@Component({
  imports: [
    CommonModule,
    TableModule,
    TagModule,
    ButtonModule,
    ToastModule
  ],
  selector: 'app-comerciantes-list',
  templateUrl: './comerciantes-list.component.html',
  providers: [ConfirmationService, MessageService]
})
export class ComerciantesListComponent {
  comerciantes: Comerciante[] = [];
  totalRecords: number = 0;
  totalPages: number = 0;
  selectedPageSize = 10;
  pageSizes = [5, 10, 15];
  isAdmin = false;
  fechaRegistro: Date | null = null;
  nombreSearch: string = '';
  subscriptions: Subscription[] = [];
  constructor(
    private comerciantesService: ComerciantesService,
    private reportesService: ReportesService,
    private confirmationService: ConfirmationService,
    private authService: AuthService,
    private messageService: MessageService
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
              // También puedes guardar información de paginación si la necesitas:
              this.totalRecords = response.data.totalCount;
              this.totalPages = response.data.totalPages;
            } else {
              console.error('Error en la respuesta:', response.message);
            }
          },
          error: (error) => {
            console.error('Error al cargar comerciantes:', error);
          }
        }));
  }

  onDelete(id: number) {
    this.confirmationService.confirm({
      message: '¿Está seguro de eliminar este comerciante?',
      accept: () => this.deleteComerciante(id)
    });
  }
  deleteComerciante(id: number) {
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
  editComerciante(id: number, updateDto: ComercianteUpdateDto) {
    this.comerciantesService.updateComerciante(id, updateDto).subscribe({
      next: () => {
        this.loadComerciantes();
        this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Comerciante actualizado correctamente' });
      },
      error: (error) => {
        console.error('Error al actualizar comerciante:', error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo actualizar el comerciante' });
      }
    });
  }

  updateEstado(id: number, estado: string) {
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

  getSeverity(status: string) {
    switch (status) {
      case 'Inactivo':
        return 'danger';
      case 'Activo':
      default:
        return 'success';
    }
  }
}
