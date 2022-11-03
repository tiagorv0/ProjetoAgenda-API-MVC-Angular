import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';

import { AuthService } from './../services/auth.service';

const NOT_AUTHORIZED = 401;

@Injectable()
export class RequestInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if(this.authService.hasToken()){
      const token = this.authService.getToken();
      const headers = new HttpHeaders().append('Authorization', `Bearer ${token}`);
      request = request.clone({headers});
    }
    return next.handle(request)
    .pipe(
      catchError((error: HttpEvent<any>) => {
      if (error instanceof HttpErrorResponse && error.status === NOT_AUTHORIZED) {
          this.authService.clearToken();
          this.router.navigate(['login']);
      }
      return throwError(() => error);
      })
    );
  }
}
