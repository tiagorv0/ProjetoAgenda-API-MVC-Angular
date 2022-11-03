import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

import { BaseParams } from './../../classes/params/base-params';
import { SearchInput } from './search-input';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.sass']
})
export class SearchComponent implements OnInit {

  @Input() options!: SearchInput[];

  @Output() searchEvent = new EventEmitter();

  form!: FormGroup;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      field: [null, [Validators.required]],
      value: [null]
    });
  }

  search(){
    const field = this.form.get('field')?.value;
    const value = this.form.get('value')?.value;
    const params = {[field]: value} as BaseParams;
    this.searchEvent.emit(params);
  }
}
