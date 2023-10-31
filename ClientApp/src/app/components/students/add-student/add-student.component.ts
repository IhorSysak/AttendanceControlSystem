import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
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

  constructor(
    private studentService: StudentsService, 
    private imagesService: ImagesService, 
    private router: Router, 
    private toastr: ToastrService, 
    private formBuilder: FormBuilder) { }

  baseApiUrl: string = environment.baseApiUrl;
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

  @ViewChild('firstName') firstName: ElementRef | undefined;

  employeeform!: FormGroup;
  ngOnInit() : void {
    // this.employeeform = this.formBuilder.group({
    //   firstName: ['', Validators.required, Validators.minLength],
    // })
   }

  addStudent() {
    // if (this.addStudentRequest.lastName === '') {
    //   this.lastNameInput?.nativeElement.classList.add('highlight');
    //   this.lastNameInput?.nativeElement.focus();
    //   return;
    // } else {
    //   this.lastNameInput?.nativeElement.classList.remove('highlight');
    // }

    console.log(this.addStudentRequest);
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
