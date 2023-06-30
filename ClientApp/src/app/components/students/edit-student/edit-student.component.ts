import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from 'src/app/models/student.model';
import { ImagesService } from 'src/app/services/images/images.service';
import { StudentsService } from 'src/app/services/students/students.service';

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
    imagePath: ''
  };

  imagePath: string = '';

  constructor(private activateRoute: ActivatedRoute, private studentService: StudentsService, private imagesService: ImagesService, private router: Router) { }

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
    this.studentDetails.imagePath = this.imagePath;
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

  uploadFile = (event: any) => {
    const files: FileList = event.target.files;
    if (files.length === 0) {
      return;
    }
    let fileToUpload: File = files[0];

    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.imagesService.storeImage(formData)
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress)
            console.log('progress');
          else if (event.type === HttpEventType.Response) {
            console.log('done');

            const body = JSON.stringify(event.body);
            const data = JSON.parse(body);
            this.imagePath = data.dbPath;
          }
        },
        error: (err: HttpErrorResponse) => console.log(err)
      });
  }
}
