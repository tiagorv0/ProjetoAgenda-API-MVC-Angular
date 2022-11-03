import { MyAccountModule } from './pages/my-account/my-account.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AgendaAdminModule } from './pages/agenda-admin/agenda-admin.module';
import { AgendaModule } from './pages/agenda/agenda.module';
import { UserModule } from './pages/user/user.module';
import { HomeModule } from './pages/home.module';
import { LoginModule } from './pages/login/login/login.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LoginModule,
    HomeModule,
    UserModule,
    AgendaModule,
    AgendaAdminModule,
    MyAccountModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
