import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Announcement, ClassAnnouncement } from '../models/announcement';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {

  private REST_API_SERVER="http://cadillacs-abp.kn01.fhict.nl/api/announcement";

  httpOptions = {
    headers: new HttpHeaders({
       'Content-Type': 'application/json'
      })
  };

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
  public updateAnnouncement(announcement:Announcement):Observable<any> {
    return this.httpClient.put(`${this.REST_API_SERVER}/Announcements/${announcement.id}`, 
      {
        "employeeId": announcement.employee.id, 
        "title": announcement.title, 
        "body": announcement.body
      }, 
      this.httpOptions)
  }

  //Announcements - POST
  public postAnnouncement(announcement:Announcement){
    this.httpClient.post(`${this.REST_API_SERVER}/Announcements`,{
      "title":`"${announcement.title}"`,
      "body":`"${announcement.body}"`,
      "employeeID":1000021
    },this.httpOptions).subscribe(
      data => {
          
      },
      error => {
          console.log("Error", error);
      }
    );              
  }

  //Announcements - DELETE
  public deleteAnnouncement(announcement:Announcement | number): Observable<Announcement>{
    const id = typeof announcement === 'number' ? announcement : announcement.id;

    return this.httpClient.delete<Announcement>(`${this.REST_API_SERVER}/Announcements/${id}`, this.httpOptions);
  }

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

  //Classes - POST

  //Classes - DELETE
  public deleteClassAnnouncement(classCourseID:string, classID:string, classSemester:number, classYear:number, announcement:Announcement | number): Observable<ClassAnnouncement>{
    const id = typeof announcement === 'number' ? announcement : announcement.id;

    return this.httpClient.delete<ClassAnnouncement>(`${this.REST_API_SERVER}/courses/${classCourseID}/Classes/${classID}/${classSemester}/${classYear}/announcements/${id}`, 
      this.httpOptions);
  }

  //End Zone CLASSES

  //*****************//
}
