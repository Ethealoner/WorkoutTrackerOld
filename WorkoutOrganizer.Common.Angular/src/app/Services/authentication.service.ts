import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { AuthResponse, User, UserForAuthentication } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  baseApiUrl: string = environment.baseApiUrl;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      "ngrok-skip-browser-warning": "true"    })
  };

  constructor(private http: HttpClient) { }

  Login(credentials: UserForAuthentication): Observable<AuthResponse> {
    console.log("Hello");
    console.log(this.baseApiUrl);
    return this.http.post<AuthResponse>(this.baseApiUrl + `api/user/login`, credentials, this.httpOptions)
  }

  RegisterUser(user: User): Observable<User> {
    console.log("Hello2");
    return this.http.post<User>(this.baseApiUrl + 'api/user', user, this.httpOptions);
  }

}
