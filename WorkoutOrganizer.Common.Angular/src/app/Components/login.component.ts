import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthResponse, UserForAuthentication } from '../Models/user.model';
import { AuthenticationService } from '../Services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean = false;
  isUserLoggedIn: boolean = false;
  credentials: UserForAuthentication = { username: '', password: '' };

  constructor(
    private router: Router,
    private http: HttpClient,
    private authenticationService: AuthenticationService,
    private jwtHelper: JwtHelperService) { }

  ngOnInit(): void {
    if (localStorage.getItem("userId") == null) {
      this.isUserLoggedIn = false;
    }
  }

  LoginUser(loginForm: NgForm): void {
    if (loginForm.valid) {
      this.authenticationService.Login(this.credentials)
        .subscribe({
          next: (response: AuthResponse) => {
            const token = response.token;
            localStorage.setItem("jwt", token);
            localStorage.setItem("userId", response.userId.toString())
            this.invalidLogin = false;
            this.isUserLoggedIn = true;
            this.router.navigate(['/workoutSession']);
            console.log("logged in " + localStorage.getItem("userId"));

          },
          error: (err: HttpErrorResponse) => {
            this.isUserLoggedIn = false;
            this.invalidLogin = true
            console.log(err);
          }
        })
    }
  }

  isUserAuthenticated(): boolean {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    return false;
  }

  Logout(): void {
    localStorage.removeItem("jwt");
    localStorage.removeItem("username");
    this.isUserLoggedIn = false;
  }


}
