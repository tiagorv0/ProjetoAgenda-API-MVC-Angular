import { Observable, take } from 'rxjs';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';

import { SearchInput } from './../../shared/components/search/search-input';
import { AgendaAdminService } from './../../shared/services/agenda-admin.service';
import { TableColumn } from './../../shared/components/table/table-column';
import { ContactAdmin } from 'src/app/shared/entities/contact-admin';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
import { ConfirmModalData } from 'src/app/shared/components/modal/confirm-modal/confirm-modal-data';
import { ConfirmModalComponent } from 'src/app/shared/components/modal/confirm-modal/confirm-modal.component';
import { ApiErrorHandler } from 'src/app/shared/classes/error/api-error-handler';
import { ApiBaseError } from 'src/app/shared/classes/error/api-base-error';
import { TableMenuOptions } from 'src/app/shared/classes/table-menu-options';
import { PaginationResponse } from 'src/app/shared/classes/pagination/pagination-response';

@Component({
  selector: 'app-agenda-admin',
  templateUrl: './agenda-admin.component.html',
  styleUrls: ['./agenda-admin.component.sass']
})
export class AgendaAdminComponent implements OnInit {

  contactsAdmin$!: Observable<PaginationResponse<ContactAdmin>>;
  tableOptions!: TableMenuOptions;
  agendaAdminColumns!: TableColumn[];
  optionsSearch: SearchInput[] = [
    {viewValue: 'NOME', value: 'name'},
    {viewValue: 'DDD', value: 'ddd'},
    {viewValue: 'NÚMERO', value: 'number'},
    {viewValue: 'USUÁRIO', value: 'user'},
  ];

  constructor(private agendaAdminService: AgendaAdminService,
              private router: Router,
              private dialog: MatDialog,
              private cdRef: ChangeDetectorRef,
              private snackBar: MatSnackBar) { }

  async ngOnInit() {
    await this.refreshAsync();
  }

  async refreshAsync(){
    this.contactsAdmin$ = this.agendaAdminService.getAsync(new BaseParams);
    this.initColums();
    this.setTableConfig();
    this.cdRef.detectChanges();
  }

  initColums(){
    this.agendaAdminColumns = [
      { name: 'ID', dataKey: 'id'},
      { name: 'NOME', dataKey: 'name'},
      { name: 'USUÁRIO', dataKey: 'user'},
    ]
  }

  async search(event: BaseParams){
    this.contactsAdmin$ = this.agendaAdminService.getAsync(event);
    this.cdRef.detectChanges();
  }

  async deleteContactAsync(id: number){
    const data: ConfirmModalData = {
      title: 'Excluir Contato',
      message: 'Deseja excluir esse Contato?'
    };
    const dialogRef = this.dialog.open(ConfirmModalComponent, {data});
    dialogRef.afterClosed().subscribe(async result => {
      if(result){
        await this.agendaAdminService.deleteAsync(id).pipe(take(1)).subscribe({
          next: () => {
            this.snackBar.open("Contato excluído com Sucesso!", undefined, {duration: 3000});
            this.refreshAsync();
          },
          error: ({error}) => {
            ApiErrorHandler(this.snackBar, error as ApiBaseError);
          }
        });
      }
    });
  }

  ContactAdminForm(id: number){
    this.router.navigate(['admin/agenda/edit', id]);
  }

  setTableConfig(): void {
    this.tableOptions = {
      deleteAction: (id) => this.deleteContactAsync(id),
      editAction: (id) => this.ContactAdminForm(id),
    };
  }

}
