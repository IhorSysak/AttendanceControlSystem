import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-logic',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: User = {
    userName: '',
    password: ''
  }

  ngOnInit() {
    this.authService.logout();
   }

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) { }

  loginUser() {
    this.authService.login(this.user)
      .subscribe({
        next: (response) => {
          localStorage.setItem('authToken', response);
          this.router.navigate(['students']);
        },
        error: (response) => {
          console.log(response);
        }
      })
  }
}
