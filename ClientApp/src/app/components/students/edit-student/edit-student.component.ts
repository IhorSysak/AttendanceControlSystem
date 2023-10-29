import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Student } from 'src/app/models/student.model';
import { ImagesService } from 'src/app/services/images/images.service';
import { StudentsService } from 'src/app/services/students/students.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {

  baseApiUrl: string = environment.baseApiUrl;

  studentDetails: Student = {
    id: '',
    firstName: '',
    lastName: '',
    middleName: '',
    course: 0,
    group: '',
    email: '',
    imagePath: ''
  };

  previosImagePath: string = '';

  constructor(
    private activateRoute: ActivatedRoute,
    private studentService: StudentsService, 
    private imagesService: ImagesService, 
    private router: Router, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.activateRoute.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.studentService.getStudent(id)
            .subscribe({
              next: (response) => {
                this.studentDetails = response;
                console.log(this.studentDetails);
              },
              error: (response) => {
                console.log(response);
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
          this.toastr.success('The student was successfully updated');
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
          this.toastr.warning('The student was successfully removed');
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

    this.imagesService.deleteImage(this.studentDetails.imagePath)
        .subscribe({
          next: (response) => {
            console.log(response);
          },
          error: (response) => {
            console.log(response);
          }
        });

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
            this.studentDetails.imagePath = data.dbPath;
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
