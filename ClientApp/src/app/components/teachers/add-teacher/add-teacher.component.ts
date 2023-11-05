import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RegisterRequest } from 'src/app/models/registerRequest.model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-add-teacher',
  templateUrl: './add-teacher.component.html',
  styleUrls: ['./add-teacher.component.css']
})
export class AddTeacherComponent implements OnInit {
  addTeacherRequest: RegisterRequest = {
    userName: '',
    password: '',
    role: ''
  };

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void { }

  addTeacher() {
    this.addTeacherRequest.role = 'Teacher';
    this.authService.register(this.addTeacherRequest)
      .subscribe({
        next: (response) => {
          this.toastr.success('Teacher was successfully added');
          this.router.navigate(['teachers']);
        },
        error: (error) => {
          console.log(error);
        }
      });
  }
}
