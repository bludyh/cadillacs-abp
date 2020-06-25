import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Attachment } from '../models/attachment';
import { Course } from '../models/course';
import { Class } from '../models/class';
import { StudyMaterial } from '../models/studyMaterial';
import { Schedule } from '../models/schedule';
import {Room} from "../models/room";

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/schedule";

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient:HttpClient) { }

  //Zone CLASSES
  //Classes - GET
  public getSchedules(classCourseID:string,classID:string,classSemester:number,classYear:number):Observable<Schedule[]>{
    return this.httpClient.get<Schedule[]>(`${this.REST_API_SERVER}/courses/${classCourseID}/Classes/${classID}/${classSemester}/${classYear}/schedules`);
  }
  //Classes - POST
  public postSchedule(theClass:Class, timeSlotId:number, date:Date, room:Room){
     this.httpClient.post(`${this.REST_API_SERVER}/courses/${theClass.courseID}/Classes/${theClass.id}/${theClass.semester}/${theClass.year}/schedules`,{
      "timeSlotId": timeSlotId,
      "date": Date,
      "roomId": room.id,
      "roomBuildingId": room.building.id}
      ,this.httpOptions).subscribe(
      data => {

      },
       error => {
        console.log("Error", error);
       }
    );
  }

  //Classes - PUT

  //Classes - DELETE

  //End Zone CLASSES

  //*****************//

}
