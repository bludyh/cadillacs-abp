import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Student } from './models/student';
import { Enrollment } from './models/enrollment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudyProgressService {

  private REST_API_SERVER="http://localhost/api/studyprogress";

  constructor(private httpClient:HttpClient) { }

  //Students
  public getEnrollments(studentID:number):Observable<Enrollment[]>{
    return this.httpClient.get<Enrollment[]>(`${this.REST_API_SERVER}/Students/${studentID}/enrollments`);
  }



}
