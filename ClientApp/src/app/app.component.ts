import { Component, DoCheck } from '@angular/core';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements DoCheck {

  title = 'ClientApp';

  isLogin: boolean = false;

  constructor() { }

  ngDoCheck() {
    const token = localStorage.getItem('authToken');
    token ? this.isLogin = true : this.isLogin = false;
  }

  hasRole(role: string): boolean {
    const token = localStorage.getItem('authToken');

    if (token) {
      const decodedToken: any = jwt_decode(token);
      console.log('asda', decodedToken)
      const body = JSON.stringify(decodedToken);
      const object = JSON.parse(body);

      const userRoles: string[] = decodedToken.roles;

      return userRoles.includes(role);
    }

    return false;
  }
}

