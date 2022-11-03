import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { Enumeration } from '../entities/enumeration';
import { ContactAdmin } from './../entities/contact-admin';
import { ApiBaseService } from './api-base.service';

@Injectable({
  providedIn: 'root'
})
export class AgendaAdminService extends ApiBaseService<ContactAdmin>{

  constructor(protected override httpClient: HttpClient) {
    super(httpClient, "admin/agenda");
   }

   getPhoneTypes(): Observable<Enumeration[]>{
    return this.httpClient.get<Enumeration[]>(`${this.API}/${this.route}/phone-types`);
  }
}
