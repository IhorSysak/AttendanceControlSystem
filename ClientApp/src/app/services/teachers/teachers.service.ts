import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Teachers } from 'src/app/models/teacher.model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TeachersService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getAllTeachers(): Observable<Teachers[]> {
    return this.http.get<Teachers[]>(this.baseApiUrl + '/api/Teacher');
  }

  deleteTeacher(id: string): Observable<string> {
    return this.http.delete<string>(this.baseApiUrl + '/api/Teacher/' + id);
  }
}
