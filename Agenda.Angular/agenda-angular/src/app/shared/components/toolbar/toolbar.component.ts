import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { Roles } from './../../enums/roles';
import { AuthService } from './../../services/auth.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.sass']
})
export class ToolbarComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  isAdmin(){
    return this.authService.getRole() == Roles.ADMIN;
  }

  logout(){
    this.authService.clearToken();
    this.router.navigate(['login']);
  }
}
