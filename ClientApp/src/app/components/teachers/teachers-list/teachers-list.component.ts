import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Teachers } from 'src/app/models/teacher.model';
import { TeachersService } from 'src/app/services/teachers/teachers.service';

@Component({
  selector: 'app-teachers-list',
  templateUrl: './teachers-list.component.html',
  styleUrls: ['./teachers-list.component.css']
})
export class TeachersListComponent implements OnInit {
  teachers: Teachers[] = [];

  constructor(private teacherService: TeachersService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.teacherService.getAllTeachers()
      .subscribe({
        next: (teachers) => {
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
          console.log(response);
          const index = this.teachers.findIndex(t => t.id === id);
          if(index) {
            this.teachers.splice(index, 1);
          }
          this.toastr.warning('The teacher was successfully removed');
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
