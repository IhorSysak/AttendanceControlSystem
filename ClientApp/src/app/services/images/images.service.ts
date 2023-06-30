import { HttpClient, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  storeImage(studentFormData: FormData): Observable<HttpEvent<Object>> {
    return this.http.post(this.baseApiUrl + '/api/Upload', studentFormData, { reportProgress: true, observe: 'events' })
  }
}
