import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Student } from 'src/app/models/student.model';
import { StudentsService } from 'src/app/services/students.service';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {
  addStudentRequest: Student = {
    id: '',
    fullName: '',
    course: 1,
    group: '',
    file: ''
  };

  constructor(private studentService: StudentsService, private router: Router) { }

  ngOnInit(): void {}

  addStudent() {
    console.log(this.addStudentRequest);
    this.studentService.createStudent(this.addStudentRequest)
      .subscribe({
        next: (response) => {
          this.router.navigate(['students']);
        },
        error: (error) => {
          console.log(error);
        }
      });
  }
}
