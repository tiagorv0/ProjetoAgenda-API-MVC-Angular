import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

import { PaginationResponse } from 'src/app/shared/classes/pagination/pagination-response';
import { Base } from './../../entities/base';
import { TableMenuOptions } from './../../classes/table-menu-options';
import { TableColumn } from './table-column';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.sass']
})
export class TableComponent implements OnInit, AfterViewInit  {

  dataSource!: MatTableDataSource<any>;
  displayedColumns!: string[];
  @ViewChild(MatPaginator, {static: false}) matPaginator!: MatPaginator;



  @Input() columns!: TableColumn[];
  @Input() menuOptions!: TableMenuOptions;
  @Input() set tableData(tableData: PaginationResponse<Base>) {
    this.paginationTotal = tableData.total;
    this.setTableDataSource(tableData.data);
  }

  @Input() paginationSizes: number[] = [2, 4, 6];
  paginationTotal!: number;

  constructor() { }

  ngOnInit(): void {
    let columnNames = this.columns.map((tableColumn: TableColumn) => tableColumn.name);
    columnNames = columnNames.concat(['actions']);
    this.displayedColumns = columnNames;
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.matPaginator;
  }

  setTableDataSource(data: Base[]) {
    this.dataSource = new MatTableDataSource(data);
    this.dataSource.paginator = this.matPaginator;
  }

  deleteOption(id: number){
    this.menuOptions.deleteAction(id);
  }

  editOption(id: number){
    this.menuOptions.editAction(id);
  }

}
