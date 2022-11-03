import { Observable } from 'rxjs';
import { TokenProps } from './../classes/token-props';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import jwt_decode from 'jwt-decode';
import { Token } from '../classes/token';
import { Login } from './../classes/login/login';
import { environment } from './../../../environments/environment';

const API = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  login(login: Login): Observable<Token>{
    return this.httpClient.post<Token>(`${API}/auth`, login);
  }

  getToken(){
    return localStorage.getItem("@token") ?? '';
  }

  getRole(){
    return localStorage.getItem("@role") ?? '';
  }

  setToken(token: string){
    const { role } = jwt_decode(token) as TokenProps;
    localStorage.setItem("@token", token);
    localStorage.setItem("@role", role);
  }

  clearToken(){
    localStorage.removeItem("@token");
    localStorage.removeItem("@role");
  }

  hasToken(){
    return !!this.getToken();
  }
}
