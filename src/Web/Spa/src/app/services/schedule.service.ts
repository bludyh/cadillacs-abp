import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Attachment } from '../models/attachment';
import { Course } from '../models/course';
import { Class } from '../models/class';
import { StudyMaterial } from '../models/studyMaterial';
import { Schedule } from '../models/schedule';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/schedule";

  constructor(private httpClient:HttpClient) { }

  //Zone CLASSES
  //Classes - GET
  public getSchedules(classCourseID:string,classID:string,classSemester:number,classYear:number):Observable<Schedule[]>{
    return this.httpClient.get<Schedule[]>(`${this.REST_API_SERVER}/courses/${classCourseID}/Classes/${classID}/${classSemester}/${classYear}/schedules`);
  }
  //Classes - POST

  //Classes - PUT

  //Classes - DELETE

  //End Zone CLASSES

  //*****************//

}
