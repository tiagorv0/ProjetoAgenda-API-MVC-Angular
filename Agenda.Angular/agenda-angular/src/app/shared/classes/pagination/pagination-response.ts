export interface PaginationResponse<T> {
  data: T[];
  total: number;
  skip: number;
  take: number;
}
