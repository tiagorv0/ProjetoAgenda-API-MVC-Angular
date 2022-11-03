import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ApiBaseService } from './api-base.service';
import { Contact } from '../entities/contact';
import { Enumeration } from '../entities/enumeration';

@Injectable({
  providedIn: 'root'
})
export class AgendaService extends ApiBaseService<Contact>{

  constructor(protected override httpClient: HttpClient) {
    super(httpClient, "agenda");
  }

  getPhoneTypes(): Observable<Enumeration[]>{
    return this.httpClient.get<Enumeration[]>(`${this.API}/${this.route}/phone-types`);
  }
}
