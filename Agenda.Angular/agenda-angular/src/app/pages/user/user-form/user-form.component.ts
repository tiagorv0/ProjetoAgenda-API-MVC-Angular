import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Observable, take } from 'rxjs';

import { ApiBaseError } from 'src/app/shared/classes/error/api-base-error';
import { ApiErrorHandler } from 'src/app/shared/classes/error/api-error-handler';
import { Enumeration } from 'src/app/shared/entities/enumeration';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.sass']
})
export class UserFormComponent implements OnInit {

  form!: FormGroup;
  roles$!: Observable<Enumeration[]>;
  isLoading = false;
  id!: number;
  title!: string;

  constructor(private formBuilder: FormBuilder,
              private snackBar: MatSnackBar,
              private userService: UserService,
              private route: ActivatedRoute,
              private location: Location) {
    this.form = formBuilder.group({
      id: [null],
      name: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      userRoleId: [null, [Validators.required]],
    });
   }

  async ngOnInit() {
    this.route.params.subscribe((params) => {
      this.id = params['id'] ?? 0;
    });

    if(this.id == 0){
      this.title = "Criar Usuário";
    }else{
      this.title = "Editar Usuário";
      const user$ = await this.userService.getByIdAsync(this.id)
                                          .pipe(take(1));
      user$.subscribe(user => {
        this.form.patchValue(user);
      });
    }

    this.roles$ = await this.userService.getUserRoles();
  }

  async realizeFormAsync(){
      this.isLoading = true;

      if(this.form.valid){
        this.id != 0 ? await this.updateUserAsync() :
        await this.userService.createAsync(this.form.value).pipe(take(1)).subscribe({
          next: () => {
            this.isLoading = false;
            this.snackBar.open('Usuário criado com sucesso!', undefined, { duration: 3000});
            this.location.back();
          },
          error: ({error}) => {
            this.isLoading = false;
            ApiErrorHandler(this.snackBar, error as ApiBaseError);
          }
        });
     }
  }

  async updateUserAsync(){
      await this.userService.updateAsync(this.id, this.form.value).pipe(take(1)).subscribe({
        next: () => {
          this.isLoading = false;
          this.snackBar.open('Usuário alterado com sucesso!', undefined, { duration: 3000});
          this.location.back();
        },
        error: ({error}) => {
          this.isLoading = false;
          ApiErrorHandler(this.snackBar, error as ApiBaseError);
        }
      });
  }

  return(){
    this.location.back();
  }

}
