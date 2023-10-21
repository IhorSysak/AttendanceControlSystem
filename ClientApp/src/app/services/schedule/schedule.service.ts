import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ScheduleRequest } from 'src/app/models/scheduleRequest.model';
import { ScheduleResponse } from 'src/app/models/scheduleResponse.module';
import { SubjectRequest } from 'src/app/models/subjectRequest.model';
import { SubjectResponse } from 'src/app/models/subjectResponse.module';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getSchedule(getScheduleRequest: ScheduleRequest): Observable<ScheduleResponse> {
    const params = new HttpParams()
      .set('course', getScheduleRequest.course.toString())
      .set('fullName', getScheduleRequest.fullName)
      .set('group', getScheduleRequest.group)
      .set('date', getScheduleRequest.date.toLocaleString());
    return this.http.get<ScheduleResponse>(this.baseApiUrl + '/api/Schedule/GetSchedule', {params});
  }

  getSubject(getSubjectRequest: SubjectRequest): Observable<SubjectResponse[]> {
    const params = new HttpParams()
      .set('group', getSubjectRequest.group)
      .set('date', getSubjectRequest.date.toLocaleString());
    return this.http.get<SubjectResponse[]>(this.baseApiUrl + '/api/Schedule/GetSubject', {params});
  }

}
