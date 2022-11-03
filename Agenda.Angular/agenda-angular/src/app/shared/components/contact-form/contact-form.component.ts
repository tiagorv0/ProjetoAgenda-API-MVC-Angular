import { Observable, take } from 'rxjs';
import { Location } from '@angular/common';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Component, OnInit, ChangeDetectorRef, ChangeDetectionStrategy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

import { Enumeration } from './../../entities/enumeration';
import { Phone } from './../../entities/phone';
import { AgendaService } from './../../services/agenda.service';
import { AgendaAdminService } from './../../services/agenda-admin.service';
import { UserService } from '../../services/user.service';
import { ApiBaseService } from '../../services/api-base.service';
import { User } from '../../entities/user';
import { ApiErrorHandler } from '../../classes/error/api-error-handler';
import { ApiBaseError } from '../../classes/error/api-base-error';
import { PhoneTypes } from '../../enums/phone-types';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ContactFormComponent implements OnInit {

  form!: FormGroup;
  id!: number;
  service!: ApiBaseService<any>;
  title!: string;
  isLoading = false;
  user$!: Observable<User[]>;
  phoneTypes$!: Observable<Enumeration[]>;
  isAdmin = false;

  get phonesFieldArray(): FormArray{
    return this.form.get('phones') as FormArray;
  }

  constructor(private formBuilder: FormBuilder,
              private snackBar: MatSnackBar,
              private agendaAdminService:AgendaAdminService,
              private agendaService: AgendaService,
              private userService: UserService,
              private route: ActivatedRoute,
              private location: Location,
              private cdRef: ChangeDetectorRef) {
    this.form = formBuilder.group({
      id: [null],
      name: [null, Validators.required],
      phones: formBuilder.array([])
    });
  }

  async ngOnInit() {
    await this.IsAdminAsync();
    await this.isCreateOrEditAsync();
    await this.getPhoneTypesAsync();

    this.cdRef.detectChanges();
  }

  async IsAdminAsync(){
    this.isAdmin = this.location.path().includes('admin');
    if(this.isAdmin){
      this.form.addControl('userId', new FormControl(null, [Validators.required]));
      await this.getUsersAsync();
      this.cdRef.detectChanges();
    }
    this.service = this.isAdmin ? this.agendaAdminService : this.agendaService;
  }

  async isCreateOrEditAsync(){
    this.route.params.subscribe((params) => {
      this.id = params['id'] ?? 0;
    });

    if(this.id == 0){
      this.title = "Criar Contato";
    }else{
      this.title = "Editar Contato";
      const contact$ = await this.service.getByIdAsync(this.id)
                                          .pipe(take(1));
      contact$.subscribe(contact => {
        this.form.get('id')?.setValue(contact.id);
        this.form.get('name')?.setValue(contact.name);
        contact.phones.forEach((x: Phone) => this.buildPhoneForm(x));
        if(this.isAdmin){
          this.form.get('userId')?.setValue(contact.user.id);
        }
      });
    }
  }

  async realizeFormAsync(){
    this.isLoading = true;
    if(this.form.valid){
      this.id != 0 ? await this.updateContactAsync() :
      await this.service.createAsync(this.form.value).pipe(take(1)).subscribe({
        next: () => {
          this.isLoading = false;
          this.location.back();
          this.snackBar.open('Contato criado com sucesso!', undefined, { duration: 3000});
        },
        error: ({error}) => {
          this.isLoading = false;
          ApiErrorHandler(this.snackBar, error as ApiBaseError);
        }
      });
   }
   this.isLoading = false;
  }

  async updateContactAsync(){
    await this.service.updateAsync(this.id, this.form.value).pipe(take(1)).subscribe({
      next: () => {
        this.isLoading = false;
        this.location.back();
        this.snackBar.open('Contato alterado com sucesso!', undefined, { duration: 3000});
      },
      error: ({error}) => {
        this.isLoading = false;
        ApiErrorHandler(this.snackBar, error as ApiBaseError);
      }
    });
    this.isLoading = false;
  }

  buildPhoneForm(data?: Phone){
    this.phonesFieldArray.push(
      this.formBuilder.group({
        formattedPhone: [data?.formattedPhone, [Validators.required, this.phoneValidator]],
        description: [data?.description, [Validators.required]],
        phoneTypeId: [data?.phoneType.id, [Validators.required]]
      })
    );
  }

  removePhoneForm(index: number){
    this.phonesFieldArray.removeAt(index);
  }

  return(){
    this.location.back();
  }

  async getPhoneTypesAsync(){
    this.phoneTypes$ = await this.agendaService.getPhoneTypes();
  }

  async getUsersAsync(){
    this.user$ = await this.userService.getAllAsync();
  }

  phoneValidator(control: AbstractControl): ValidationErrors | null {
    const isValid = new RegExp(/^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}/).test(control.value);
    if (isValid) {
      return null;
    }
    return { formattedPhone: { value: control.value } };
  }

  getMask(index: number): string{
    return this.phonesFieldArray.at(index).get('phoneTypeId')?.value === PhoneTypes.Cellphone
    ? '(00) 00000-0000'
    : '(00) 0000-0000';
  }
}
