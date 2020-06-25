import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Attachment } from '../models/attachment';
import { Course } from '../models/course';
import { Class } from '../models/class';
import { StudyMaterial } from '../models/studyMaterial';
import { Schedule } from '../models/schedule';
import { TimeSlot } from '../models/timeslot';
import { ClassSchedule } from '../models/classSchedule';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/schedule";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private httpClient:HttpClient) { }

  //Zone CLASSES
  //Classes - GET
  public getSchedules(classCourseID:string,classID:string,classSemester:number,classYear:number):Observable<Schedule[]>{
    return this.httpClient.get<Schedule[]>(`${this.REST_API_SERVER}/courses/${classCourseID}/Classes/${classID}/${classSemester}/${classYear}/schedules`);
  }
  //Classes - POST

  //Classes - DELETE
  public deleteSchedule(classSchedule:ClassSchedule): Observable<ClassSchedule> {
    return this.httpClient.delete<ClassSchedule>(
      `${this.REST_API_SERVER}/courses/${classSchedule.class.course.id}/Classes/${classSchedule.class.id}/${classSchedule.class.semester}/${classSchedule.class.year}/schedules?timeSlotId=${classSchedule.timeSlot.id}&date=${classSchedule.date}&roomId=${classSchedule.room.id}&buildingId=${classSchedule.room.building.id}`, 
      this.httpOptions);
  }

  //End Zone CLASSES

  //*****************//

}
