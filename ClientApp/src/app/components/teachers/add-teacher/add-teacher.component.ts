import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-add-teacher',
  templateUrl: './add-teacher.component.html',
  styleUrls: ['./add-teacher.component.css']
})
export class AddTeacherComponent implements OnInit {
  addTeacherRequest: User = {
    userName: '',
    password: ''
  };

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void { }

  addTeacher() {
    this.authService.register(this.addTeacherRequest)
      .subscribe({
        next: (response) => {
          this.router.navigate(['teachers']);
        },
        error: (error) => {
          console.log(error);
        }
      });
  }
}
