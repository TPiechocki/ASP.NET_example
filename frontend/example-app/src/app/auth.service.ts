import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private logger = new BehaviorSubject<boolean>(false);
  private loggedIn: boolean = false;

  constructor(private router: Router) {
    this.loggedIn = localStorage.getItem('access_token') != null;
    this.logger.next(this.loggedIn);
  }

  init() {
    this.loggedIn = localStorage.getItem('access_token') != null;
    this.logger.next(this.loggedIn);
  }

  isLoggedIn() : Observable<boolean> {
    return this.logger.asObservable();
  }

  login(token: string) {
    localStorage.setItem('access_token', token);
    this.loggedIn = true;
    this.logger.next(this.loggedIn);
    this.router.navigate(['satellites']);
  }

  logout() {
    localStorage.removeItem('access_token');
    this.loggedIn = false;
    this.logger.next(this.loggedIn);
    this.router.navigate(['login']);
  }
}
