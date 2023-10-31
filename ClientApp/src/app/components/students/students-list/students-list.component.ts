import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Student } from 'src/app/models/student.model';
import { StudentsService } from 'src/app/services/students/students.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.css']
})
export class StudentsListComponent implements OnInit {

  students: Student[] = [];
  filteredStudents: Student[] = [];
  baseApiUrl: string = environment.baseApiUrl;
  constructor(private studentService: StudentsService, private router: Router) { }

  ngOnInit(): void {
    this.studentService.getAllStudents()
      .subscribe({
        next: (students) => {
          this.students = students;
          this.filteredStudents = [...students];
          console.log(students);
        },
        error: (response) => {
          console.log(response);
        }
      })
  }

  filterStudents(): void {
    const courseFilter = (document.getElementById('course') as HTMLInputElement)?.value;
    const groupFilter = (document.getElementById('group') as HTMLInputElement)?.value;
    const fullNameFilter = (document.getElementById('fullName') as HTMLInputElement)?.value;

    this.filteredStudents = this.students.filter((student) => {
      const course = !courseFilter || student.course.toString() === courseFilter;
      const group = !groupFilter || student.group.toLowerCase().includes(groupFilter.toLowerCase());
      const fullName = !fullNameFilter || `${student.lastName} ${student.firstName} ${student.middleName}`.toLowerCase().includes(fullNameFilter.toLowerCase());
      return course && group && fullName;
    });
  }

  redirectToCreatingPage() : void {
    this.router.navigate(['students/add']);
  }

  createImage(path: string) {
    return `${this.baseApiUrl}/${path}`;
  }
}
