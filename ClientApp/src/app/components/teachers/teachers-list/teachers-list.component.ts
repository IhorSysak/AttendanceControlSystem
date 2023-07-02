import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Teachers } from 'src/app/models/teacher.model';
import { TeachersService } from 'src/app/services/teachers/teachers.service';

@Component({
  selector: 'app-teachers-list',
  templateUrl: './teachers-list.component.html',
  styleUrls: ['./teachers-list.component.css']
})
export class TeachersListComponent implements OnInit {
  teachers: Teachers[] = [];

  constructor(private teacherService: TeachersService, private router: Router) { }

  ngOnInit(): void {
    this.teacherService.getAllTeachers()
      .subscribe({
        next: (teachers) => {
          console.log(teachers);
          this.teachers = teachers;
        },
        error: (response) => {
          console.log(response);
        }
      })
  }

  deleteTeacher(id: string) {
    this.teacherService.deleteTeacher(id)
      .subscribe({
        next: (response) => {
          const index = this.teachers.findIndex(t => t.id === id);
          if(index) {
            this.teachers.splice(index, 1);
          }
        },
        error: (response) => {
          console.log(response);
        }
      })
  }

  redirectToCreatingPage(): void {
    this.router.navigate(['teacher/add']);
  }
}
