import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Announcement, ClassAnnouncement } from '../models/announcement';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/announcement";

  constructor(private httpClient:HttpClient) { }

  //Zone ANNOUNCEMENTS
  //Announcements - GET
  public getAnnouncements():Observable<Announcement[]>{
    return this.httpClient.get<Announcement[]>(`${this.REST_API_SERVER}/Announcements`);
  }

  public getAnnouncement(announcementID:number):Observable<Announcement>{
    return this.httpClient.get<Announcement>(`${this.REST_API_SERVER}/Announcements/${announcementID}`);
  }

  //Announcements - PUT

  //Announcements - POST

  //Announcements - DELETE

  //End Zone ANNOUNCEMENTS

  //*****************//

  //Zone CLASSES
  //Classes - GET
  public getClassAnnouncements(classCourseID:string,classID:string,classSemester:number,classYear:number):Observable<ClassAnnouncement[]>{
    return this.httpClient.get<ClassAnnouncement[]>(`${this.REST_API_SERVER}/courses/${classCourseID}/Classes/${classID}/${classSemester}/${classYear}/announcements`);
  }

  public getClassAnnouncement(classCourseID:string,classID:string,classSemester:number,classYear:number,announcementID:number):Observable<ClassAnnouncement>{
    return this.httpClient.get<ClassAnnouncement>(`${this.REST_API_SERVER}/courses/${classCourseID}/Classes/${classID}/${classSemester}/${classYear}/announcements/${announcementID}`);
  }
  //Classes - PUT

  //Classes - POST

  //Classes - DELETE

  //End Zone CLASSES

  //*****************//
}
