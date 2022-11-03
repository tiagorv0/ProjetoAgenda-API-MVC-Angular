import { RouterModule } from '@angular/router';
import { SearchModule } from './../../shared/components/search/search.module';
import { TableModule } from './../../shared/components/table/table.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from './../../material/material.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgendaComponent } from './agenda.component';



@NgModule({
  declarations: [
    AgendaComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FlexLayoutModule,
    TableModule,
    SearchModule,
    RouterModule
  ]
})
export class AgendaModule { }
