import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit { 
  user: User = {
    userName: '',
    password: ''
  }

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {}
  
  ngOnInit() {}

  registerUser() {
    this.authService.register(this.user)
      .subscribe({
        next: (response) => {
          this.router.navigate(['login']);
          this.toastr.success('The user has been successfully registered');
        },
        error: (response) => {
          console.log(response);
        }
      })
  }
}
