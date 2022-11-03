import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { lastValueFrom } from 'rxjs';

import { ApiBaseError } from './../../../shared/classes/error/api-base-error';
import { Login } from './../../../shared/classes/login/login';
import { AuthService } from './../../../shared/services/auth.service';
import { ApiErrorHandler } from 'src/app/shared/classes/error/api-error-handler';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {

  form!: FormGroup;
  isLoading = false;

  constructor(private formBuilder: FormBuilder,
              private authService: AuthService,
              private router: Router,
              private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username: [null, [Validators.required]],
      password: [null, [Validators.required]]
    })
  }

  async loginAsync(){
    try{
      this.isLoading = true;
      if(this.form.valid){
        const data = this.form.getRawValue() as Login;
        var { token } = await lastValueFrom(this.authService.login(data));
        this.authService.setToken(token);
        this.router.navigate(['']);
      }
    } catch({error}){
      ApiErrorHandler(this.snackBar, error as ApiBaseError);
    } finally{
      this.isLoading = false;
    }
  }
}
