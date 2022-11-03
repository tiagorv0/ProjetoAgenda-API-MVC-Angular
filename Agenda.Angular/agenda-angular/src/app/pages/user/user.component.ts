import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiErrorHandler } from 'src/app/shared/classes/error/api-error-handler';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable, take } from 'rxjs';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

import { ApiBaseError } from './../../shared/classes/error/api-base-error';
import { ConfirmModalComponent } from './../../shared/components/modal/confirm-modal/confirm-modal.component';
import { ConfirmModalData } from './../../shared/components/modal/confirm-modal/confirm-modal-data';
import { BaseParams } from './../../shared/classes/params/base-params';
import { SearchInput } from './../../shared/components/search/search-input';
import { TableColumn } from './../../shared/components/table/table-column';
import { UserService } from './../../shared/services/user.service';
import { User } from 'src/app/shared/entities/user';
import { TableMenuOptions } from 'src/app/shared/classes/table-menu-options';
import { PaginationResponse } from 'src/app/shared/classes/pagination/pagination-response';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.sass']
})
export class UserComponent implements OnInit {

  users$!: Observable<PaginationResponse<User>>;
  tableOptions!: TableMenuOptions;
  userColumns!: TableColumn[];
  optionsSearch: SearchInput[] = [
    { viewValue: 'NOME', value: 'name'},
    { viewValue: 'USUÁRIO', value: 'userName'},
    { viewValue: 'E-MAIL', value: 'email'},
  ];

  constructor(private userService: UserService,
              private router: Router,
              private dialog: MatDialog,
              private cdRef: ChangeDetectorRef,
              private snackBar: MatSnackBar) { }

  async ngOnInit() {
    await this.refreshAsync();
  }

  async refreshAsync(){
    this.users$ = await this.userService.getAsync(new BaseParams);
    this.initColums();
    this.setTableConfig();
    this.cdRef.detectChanges();
  }

  initColums(){
    this.userColumns = [
      { name: 'ID', dataKey: 'id'},
      { name: 'NOME', dataKey: 'name'},
      { name: 'USUÁRIO', dataKey: 'userName'},
      { name: 'E-MAIL', dataKey: 'email'},
    ]
  }

  async search(event: BaseParams){
    this.users$ = await this.userService.getAsync(event);
    this.cdRef.detectChanges();
  }

  async deleteUserAsync(id: number){
    const data: ConfirmModalData = {
      title: 'Excluir Usuário',
      message: 'Deseja excluir esse usuário?'
    };
    const dialogRef = this.dialog.open(ConfirmModalComponent, {data});
    dialogRef.afterClosed().subscribe(async result => {
      if(result){
        await this.userService.deleteAsync(id).pipe(take(1)).subscribe({
          next: () => {
            this.snackBar.open("Usuário excluído com Sucesso!", undefined, {duration: 3000});
            this.refreshAsync();
          },
          error: ({error}) => {
            ApiErrorHandler(this.snackBar, error as ApiBaseError);
          }
        });
      }
    });
  }

  userForm(id: number){
    this.router.navigate(['admin/user/edit', id]);
  }

  setTableConfig(): void {
    this.tableOptions = {
      deleteAction: (id) => this.deleteUserAsync(id),
      editAction: (id) => this.userForm(id),
    };
  }

}
