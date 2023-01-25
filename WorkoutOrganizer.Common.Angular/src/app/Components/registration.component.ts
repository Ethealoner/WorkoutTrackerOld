import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../Models/user.model';
import { AuthenticationService } from '../Services/authentication.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  user: User;
  Password: String;
  IsPasswordSame: boolean = true;
  InvalidRegistration: boolean = false;

  constructor(
    private router: Router,
    private http: HttpClient,
    private authenticationService: AuthenticationService,
  )
  {
    this.user = {
      UserId: 0,
      UserName: "",
      Email: "",
      Password: "",
      workoutSessions: []
    }
    this.Password = "";
  }

  ngOnInit(): void {

  }

  RegisterUser(RegisterForm: NgForm): void {
    if (RegisterForm.valid) {
      if (this.user.Password !== this.Password) {
        this.IsPasswordSame = false;
      } else {
        this.IsPasswordSame = true;
        this.authenticationService.RegisterUser(this.user)
          .subscribe({
            next: () => {
              this.router.navigate(['/login']);
            },
            error: (err: HttpErrorResponse) => {
              this.InvalidRegistration = true;
            }
          })
      }
    } else {
      console.log("form is invalid")
    }
  }

}
