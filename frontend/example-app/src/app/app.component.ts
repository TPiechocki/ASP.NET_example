import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth.service';
import { share } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Example app';
  isLoggedIn$!: Observable<boolean>;

  constructor(
    private authService: AuthService,
    private router: Router) {
      this.isLoggedIn$ = this.authService.isLoggedIn().pipe(share());
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['']);
  }
}
