import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Student } from 'src/app/models/student.model';
import { StudentsService } from 'src/app/services/students.service';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.css']
})
export class StudentsListComponent implements OnInit {

  students: Student[] = [];
  constructor(private studentService: StudentsService, private router: Router) { }

  ngOnInit(): void {
    this.studentService.getAllStudents()
      .subscribe({
        next: (students) => {
          console.log(students);
          this.students = students;
        },
        error: (response) => {
          console.log(response);
        }
      })
  }

  redirectToCreatingPage() : void {
    this.router.navigate(['students/add']);
  }

  createImage(path: string) {
    console.log(path);
    return `https://localhost:7138/${path}`;
  }
}
