import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';
import { Student } from 'src/app/models/student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getAllStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseApiUrl + '/api/Student');
  }

  createStudent(addStudentRequest: Student): Observable<Student> {
    return this.http.post<Student>(this.baseApiUrl + '/api/Student', addStudentRequest);
  }

  getStudent(id: string): Observable<Student> {
    return this.http.get<Student>(this.baseApiUrl + '/api/Student/' + id);
  }

  editStudent(editStudentRequest: Student): Observable<Student> {
    return this.http.put<Student>(this.baseApiUrl + '/api/Student', editStudentRequest);
  }

  deleteStudent(id: string): Observable<string> {
    return this.http.delete<string>(this.baseApiUrl + '/api/Student/' + id);
  }
}
