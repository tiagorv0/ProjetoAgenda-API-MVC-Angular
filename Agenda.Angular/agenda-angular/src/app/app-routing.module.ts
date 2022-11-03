import { MyAccountComponent } from './pages/my-account/my-account.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { ContactFormComponent } from './shared/components/contact-form/contact-form.component';
import { UserFormComponent } from './pages/user/user-form/user-form.component';
import { AuthGuard } from './shared/guard/auth.guard';
import { AgendaAdminComponent } from './pages/agenda-admin/agenda-admin.component';
import { AgendaComponent } from './pages/agenda/agenda.component';
import { UserComponent } from './pages/user/user.component';
import { HomeComponent } from './pages/home.component';
import { RequestInterceptor } from './shared/interceptor/request.interceptor';
import { LoginComponent } from './pages/login/login/login.component';
import { AuthAdminGuard } from './shared/guard/auth-admin.guard';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: '', canActivate: [AuthGuard], component: HomeComponent, children: [
    {path: 'agenda', component: AgendaComponent},
    {path: 'agenda/create', component: ContactFormComponent},
    {path: 'agenda/edit/:id', component: ContactFormComponent},
    {path: 'my-account', component: MyAccountComponent},
    {path: 'admin', canActivate: [AuthAdminGuard], children: [
      {path: 'user', component: UserComponent},
      {path: 'user/create', component: UserFormComponent},
      {path: 'user/edit/:id', component: UserFormComponent},
      {path: 'agenda', component: AgendaAdminComponent},
      {path: 'agenda/create', component: ContactFormComponent},
      {path: 'agenda/edit/:id', component: ContactFormComponent},
      ]},
    ]},
  {path: '**', redirectTo: ''},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptor,
      multi: true,
    }]
})
export class AppRoutingModule { }
