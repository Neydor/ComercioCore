import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Municipio } from '@core/models/municipio.model';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class MunicipiosService {
  private apiUrl = `${environment.apiUrl}/api/municipios`;

  constructor(private http: HttpClient) {}

  getMunicipios(): Observable<Municipio[]> {
    return this.http.get<Municipio[]>(this.apiUrl);
  }
}
