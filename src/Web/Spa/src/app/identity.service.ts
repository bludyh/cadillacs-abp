import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from './models/student';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  private REST_API_SERVER="http://localhost/api/identity";
  
  constructor(private httpClient:HttpClient) { }

  //Students
  public getStudent(studentID:number):Observable<Student>{
    return this.httpClient.get<Student>(`${this.REST_API_SERVER}/Students/${studentID}`);
  }
}
