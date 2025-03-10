import { HttpClient } from "@angular/common/http";
import { Comerciante, ComercianteFilterDto, ComercianteUpdateDto, ComercianteUpdateStatusDto } from "@core/models/comerciante.model";
import { Observable } from "rxjs";
import { environment } from "@env/environment";
import { Injectable } from "@angular/core";
import { ApiResponse, PaginatedResult } from "@core/models/api-response.model";

@Injectable({ providedIn: 'root' })
export class ComerciantesService {
  private apiUrl = `${environment.apiUrl}/api/comerciantes`;

  constructor(private http: HttpClient) {}

  getComerciantes(params: ComercianteFilterDto): Observable<ApiResponse<PaginatedResult<Comerciante>>> {
    const queryParams: Record<string, string> = {};
    if (params.page && params.page.toString() !== '') {
      queryParams['PageNumber'] = params.page.toString();
    } else {
      queryParams['PageNumber'] = '1'; // Valor por defecto
    }

    if (params.pageSize && params.pageSize.toString() !== '') {
      queryParams['PageSize'] = params.pageSize.toString();
    } else {
      queryParams['PageSize'] = '10'; // Valor por defecto
    }

    if (params.estado && params.estado.trim() !== '') {
      queryParams['Estado'] = params.estado;
    }

    if (params.nombre && params.nombre.trim() !== '') {
      queryParams['Nombre'] = params.nombre;
    }

    if (params.fechaRegistro && params.fechaRegistro instanceof Date && !isNaN(params.fechaRegistro.getTime())) {
      queryParams['FechaRegistro'] = params.fechaRegistro.toISOString();
    }

    const dataparams = new URLSearchParams(queryParams);

    return this.http.get<ApiResponse<PaginatedResult<Comerciante>>>(`${this.apiUrl}?${dataparams.toString()}`);
  }

  deleteComerciante(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  updateComerciante(id: number, updateDto: ComercianteUpdateDto): Observable<Comerciante> {
    return this.http.put<Comerciante>(`${this.apiUrl}/${id}`, updateDto);
  }

  updateEstado(id: number, updateStatusDto: ComercianteUpdateStatusDto): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/estado`, updateStatusDto);
  }
}
