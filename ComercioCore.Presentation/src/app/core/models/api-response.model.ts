export interface ApiResponse<T> {
  data: T;
  success: boolean;
  message: string | null;
  errors: string[] | null;
}

export interface PaginatedResult<T> {
  data: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
}
