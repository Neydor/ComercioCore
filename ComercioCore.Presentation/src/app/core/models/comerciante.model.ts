export interface Comerciante {
  id: string;
  nombreRazonSocial: string;
  telefono?: string;
  correoElectronico?: string;
  fechaRegistro: Date;
  cantidadEstablecimientos: number;
  estado: string;
  municipioId: string;
  municipio: Municipio;
  poseeEstablecimientos: boolean;
  establecimientos?: Establecimiento[];
}

export interface Municipio {
  id: string;
  nombre: string;
}
export interface Establecimiento {
  id: number;
  comercianteId: number;
  nombre: string;
  ingresos: number;
  empleados: number;
}

export interface FiltrosComerciante {
  pagina: number;
  elementosPorPagina: number;
  busqueda?: string;
  estado?: string;
}


//request
export interface ComercianteFilterDto {
  page: number | null;
  pageSize: number | null;
  estado: string;
  fechaRegistro?: Date | null;
  nombre: string;
}

// DTO for updating comerciante status
export interface ComercianteUpdateStatusDto {
  estado: string;
}

// DTO for updating comerciante details
export interface ComercianteUpdateDto {
  nombre?: string;
  razonSocial?: string;
  telefono?: string;
  correoElectronico?: string;
  municipioId?: string;
  estado?: string;
}
