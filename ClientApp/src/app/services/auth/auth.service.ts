import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { User } from 'src/app/models/user.model';
import { Observable } from 'rxjs';
import jwt_decode from 'jwt-decode';

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

  isLogin(): boolean {
    const token = localStorage.getItem('authToken');
    return token ? true : false;
  }

  roleMatch(allowedRoles: string[]): boolean {
    const token: any = localStorage.getItem('authToken');
    if(token) {
      const decodedToken: any = jwt_decode(token);
      const userRoles: string[] = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      return allowedRoles.some(role => userRoles.includes(role));
    }
    return false;
  }

  logout() {
    localStorage.clear();
  }
}
