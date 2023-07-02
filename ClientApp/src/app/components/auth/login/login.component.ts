import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-logic',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LogicComponent implements OnInit {
  user: User = {
    userName: '',
    password: ''
  }

  ngOnInit() { }

  constructor(private authService: AuthService, private router: Router) { }

  loginUser() {
    this.authService.login(this.user)
      .subscribe({
        next: (response) => {
          console.log('login', response);
          localStorage.setItem('authToken', response);
          this.router.navigate(['students']);
        },
        error: (response) => {
          console.log(response);
        }
      })
  }
}
