import { Router } from '@angular/router';
import { Observable, take } from 'rxjs';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { AgendaService } from './../../shared/services/agenda.service';
import { TableColumn } from './../../shared/components/table/table-column';
import { Contact } from './../../shared/entities/contact';
import { SearchInput } from 'src/app/shared/components/search/search-input';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
import { ConfirmModalData } from 'src/app/shared/components/modal/confirm-modal/confirm-modal-data';
import { ConfirmModalComponent } from 'src/app/shared/components/modal/confirm-modal/confirm-modal.component';
import { ApiErrorHandler } from 'src/app/shared/classes/error/api-error-handler';
import { ApiBaseError } from 'src/app/shared/classes/error/api-base-error';
import { TableMenuOptions } from 'src/app/shared/classes/table-menu-options';
import { PaginationResponse } from 'src/app/shared/classes/pagination/pagination-response';

@Component({
  selector: 'app-agenda',
  templateUrl: './agenda.component.html',
  styleUrls: ['./agenda.component.sass']
})
export class AgendaComponent implements OnInit {

  contacts$!: Observable<PaginationResponse<Contact>>;
  tableOptions!: TableMenuOptions;
  agendaColumns!: TableColumn[];
  optionsSearch: SearchInput[] = [
    {viewValue: 'NOME', value: 'name'},
    {viewValue: 'DDD', value: 'ddd'},
    {viewValue: 'NÚMERO', value: 'number'},
  ];

  constructor(private agendaService: AgendaService,
              private router: Router,
              private dialog: MatDialog,
              private cdRef: ChangeDetectorRef,
              private snackBar: MatSnackBar) { }

  async ngOnInit(){
    this.refreshAsync();
  }

  async refreshAsync(){
    this.contacts$ = await this.agendaService.getAsync(new BaseParams);
    this.initColumns();
    this.setTableConfig();
    this.cdRef.detectChanges();
  }

  initColumns(){
    this.agendaColumns = [
      { name: 'ID', dataKey: 'id'},
      { name: 'NOME', dataKey: 'name'},
    ]
  }

  async search(event: BaseParams){
    this.contacts$ = await this.agendaService.getAsync(event);
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
        await this.agendaService.deleteAsync(id).pipe(take(1)).subscribe({
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

  ContactForm(id: number){
    this.router.navigate(['agenda/edit', id]);
  }

  setTableConfig(): void {
    this.tableOptions = {
      deleteAction: (id) => this.deleteContactAsync(id),
      editAction: (id) => this.ContactForm(id),
    };
  }

}
