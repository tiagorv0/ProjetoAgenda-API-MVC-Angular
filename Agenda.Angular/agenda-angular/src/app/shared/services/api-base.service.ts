import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { BaseParams } from './../classes/params/base-params';
import { environment } from './../../../environments/environment';
import { PaginationResponse } from '../classes/pagination/pagination-response';


@Injectable({
  providedIn: 'root'
})
export class ApiBaseService<T> {
  API = environment.apiUrl;

  constructor(protected httpClient: HttpClient, @Inject("route") protected route: string) { }

  createAsync(data: T): Observable<T>{
    return this.httpClient.post<T>(`${this.API}/${this.route}`, data);
  }

  updateAsync(id: number, data: T): Observable<T>{
    return this.httpClient.put<T>(`${this.API}/${this.route}/${id}`, data);
  }

  deleteAsync(id: number): Observable<void>{
    return this.httpClient.delete<void>(`${this.API}/${this.route}/${id}`);
  }

  getByIdAsync(id: number): Observable<T>{
    return this.httpClient.get<T>(`${this.API}/${this.route}/${id}`);
  }

  getAllAsync(): Observable<T[]>{
    return this.httpClient.get<T[]>(`${this.API}/${this.route}`);
  }

  getAsync(params: BaseParams): Observable<PaginationResponse<T>>{
    return this.httpClient.get<PaginationResponse<T>>(`${this.API}/${this.route}/search`, { params });
  }
}
