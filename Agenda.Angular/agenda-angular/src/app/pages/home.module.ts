import { TableModule } from './../shared/components/table/table.module';
import { ToolbarModule } from './../shared/components/toolbar/toolbar.module';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { MaterialModule } from 'src/app/material/material.module';



@NgModule({
  declarations: [
    HomeComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    ToolbarModule,
    TableModule,
  ]
})
export class HomeModule { }
