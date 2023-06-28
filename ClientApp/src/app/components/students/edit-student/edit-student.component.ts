import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from 'src/app/models/student.model';
import { StudentsService } from 'src/app/services/students.service';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {
  studentDetails: Student = {
    id: '',
    fullName: '',
    course: 0,
    group: '',
    file: ''
  };

  constructor(private activateRoute: ActivatedRoute, private studentService: StudentsService, private router: Router) { }

  ngOnInit(): void {
    this.activateRoute.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          this.studentService.getStudent(id)
            .subscribe({
              next: (response) => {
                this.studentDetails = response;
              }
            })
        }
      }
    });
  }

  updateStudent() {
    this.studentService.editStudent(this.studentDetails)
      .subscribe({
        next: (response) => {
          this.router.navigate(['students']);
        },
        error: (response) => {
          console.log(response);
        }
      })
  }

  deleteStudent(id: string) {
    this.studentService.deleteStudent(id)
      .subscribe({
        next: (response) => {
          this.router.navigate(['students']);
        },
        error: (response) => {
          console.log(response);
        }
      })
  }
}
