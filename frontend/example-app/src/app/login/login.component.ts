import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService as AuthServiceBackend } from '@example/webapi-contract';

import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(
    private router: Router,
    private authServiceBackend: AuthServiceBackend,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.authService.logout();
  }

  login() {
    this.authServiceBackend.login(this.model).subscribe(response => {
      this.authService.login(response);
    });
  }
}
