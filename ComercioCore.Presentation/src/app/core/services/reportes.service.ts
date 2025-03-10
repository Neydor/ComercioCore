
// src/app/core/servicios/exportar.servicio.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportesService {
  private readonly API_URL = `${environment.apiUrl}/api/exportar`;

  constructor(private http: HttpClient) {}

  exportarComerciantesCSV(): Observable<Blob> {
    return this.http.get(`${this.API_URL}/Reportes/comerciantes-activos`, { responseType: 'blob' });
  }
}
