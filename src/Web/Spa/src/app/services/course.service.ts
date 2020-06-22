import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from '../models/course';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/course";

  constructor(private httpClient:HttpClient) { }

  public getCourses():Observable<Course[]>{
    return this.httpClient.get<Course[]>(`${this.REST_API_SERVER}/Courses`);
  }
}
