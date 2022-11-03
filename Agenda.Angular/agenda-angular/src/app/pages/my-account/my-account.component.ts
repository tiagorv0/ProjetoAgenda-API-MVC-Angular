import { AuthService } from './../../shared/services/auth.service';
import { Location } from '@angular/common';
import { Observable, take } from 'rxjs';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { UserService } from 'src/app/shared/services/user.service';
import { User } from 'src/app/shared/entities/user';
import { Enumeration } from 'src/app/shared/entities/enumeration';
import { ApiErrorHandler } from 'src/app/shared/classes/error/api-error-handler';
import { ApiBaseError } from 'src/app/shared/classes/error/api-base-error';
import { Roles } from 'src/app/shared/enums/roles';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.sass']
})
export class MyAccountComponent implements OnInit {

  form!: FormGroup;
  user$!: Observable<User>;
  roles$!: Observable<Enumeration[]>;
  isLoading = false;

  constructor(private userService: UserService,
              private cdRef: ChangeDetectorRef,
              private snackBar: MatSnackBar,
              private formBuilder: FormBuilder,
              private location: Location,
              private authService: AuthService) {
    this.form = formBuilder.group({
      id: [null],
      name: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      userRoleId: [null, [Validators.required]],
    })
  }

  async ngOnInit(): Promise<void> {
    this.userService.getOwnUserAsync().pipe(take(1)).subscribe(
      user => {
        this.form.patchValue(user);
      }
    );
    this.roles$ = await this.userService.getUserRoles();
    this.cdRef.detectChanges();
  }

  updateUser(): void{
    this.isLoading = true;
    if(this.form.valid){
      const id = this.form.get('id')?.value as number;
      this.userService.updateAsync(id, this.form.value).pipe(take(1)).subscribe({
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
  }

  isAdmin(): boolean{
    return this.authService.getRole() == Roles.ADMIN
  }

  return(): void{
    this.location.back();
  }

}
