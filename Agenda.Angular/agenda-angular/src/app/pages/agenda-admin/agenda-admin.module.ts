import { RouterModule } from '@angular/router';
import { ContactFormModule } from './../../shared/components/contact-form/contact-form.module';
import { SearchModule } from './../../shared/components/search/search.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgendaAdminComponent } from './agenda-admin.component';
import { MaterialModule } from 'src/app/material/material.module';
import { TableModule } from 'src/app/shared/components/table/table.module';
import { FlexLayoutModule } from '@angular/flex-layout';



@NgModule({
  declarations: [
    AgendaAdminComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FlexLayoutModule,
    TableModule,
    SearchModule,
    ContactFormModule,
    RouterModule
  ]
})
export class AgendaAdminModule { }
