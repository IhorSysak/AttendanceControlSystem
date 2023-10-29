import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Student } from 'src/app/models/student.model';
import { ImagesService } from 'src/app/services/images/images.service';
import { StudentsService } from 'src/app/services/students/students.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {
  addStudentRequest: Student = {
    id: '',
    firstName: '',
    lastName: '',
    middleName: '',
    course: 1,
    group: '',
    email: '',
    imagePath: ''
  };

  baseApiUrl: string = environment.baseApiUrl;
  //imagePath: string = '';

  constructor(private studentService: StudentsService, private imagesService: ImagesService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void { }

  addStudent() {
    console.log(this.addStudentRequest);
    //this.addStudentRequest.imagePath = this.imagePath;
    this.studentService.createStudent(this.addStudentRequest)
      .subscribe({
        next: (response) => {
          this.router.navigate(['students']);
          this.toastr.success('The student was successfully added');
        },
        error: (error) => {
          console.log(error);
        }
      });
  }

  uploadFile = (event: any) => {
    const files: FileList = event.target.files;
    if (files.length === 0) {
      return;
    }

    if(this.addStudentRequest.imagePath !== '') {
      this.imagesService.deleteImage(this.addStudentRequest.imagePath)
        .subscribe({
          next: (response) => {
            console.log(response);
          },
          error: (response) => {
            console.log(response);
          }
        });
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
            this.addStudentRequest.imagePath = data.dbPath;
          }
        },
        error: (err: HttpErrorResponse) => console.log(err)
      });
  }

  createImage(path: string) {
    if(path === '') {
      return;
    }
    return `${this.baseApiUrl}/${path}`;
  }

  openFileInput(): void {
    const fileInput = document.getElementById('file') as HTMLInputElement;
    fileInput.click();
  }
}
