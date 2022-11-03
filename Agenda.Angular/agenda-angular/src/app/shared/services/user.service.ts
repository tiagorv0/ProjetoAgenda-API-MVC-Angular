import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { User } from './../entities/user';
import { ApiBaseService } from './api-base.service';
import { Enumeration } from '../entities/enumeration';

@Injectable({
  providedIn: 'root'
})
export class UserService extends ApiBaseService<User>{
  teste: Enumeration[] = [];

  constructor(protected override httpClient: HttpClient) {
    super(httpClient, "admin/user")
  }

  getUserRoles(): Observable<Enumeration[]>{
    return this.httpClient.get<Enumeration[]>(`${this.API}/${this.route}/user-roles`);
  }

  getOwnUserAsync(): Observable<User>{
    return this.httpClient.get<User>(`${this.API}/${this.route}/get-own-user`);
  }
}
