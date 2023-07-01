import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { User } from 'src/app/models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  register(registerRequest: User): Observable<User> {
    return this.http.post<User>(this.baseApiUrl + '/api/Auth/register', registerRequest);
  }

  login(loginRequest: User): Observable<string> {
    return this.http.post(this.baseApiUrl + '/api/Auth/login', loginRequest, {responseType: 'text'});
  }
}
