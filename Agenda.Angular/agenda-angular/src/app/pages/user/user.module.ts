import { UserFormComponent } from './user-form/user-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ConfirmModalModule } from './../../shared/components/modal/confirm-modal/confirm-modal.module';
import { SearchModule } from './../../shared/components/search/search.module';
import { TableModule } from './../../shared/components/table/table.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from 'src/app/material/material.module';

@NgModule({
  declarations: [
    UserComponent,
    UserFormComponent
  ],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MaterialModule,
    TableModule,
    SearchModule,
    ConfirmModalModule,
    RouterModule,
    ReactiveFormsModule
  ]

})
export class UserModule { }
