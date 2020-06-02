import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Announcement } from './models/announcement';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {

  private REST_API_SERVER="http://localhost/api/announcement";

  constructor(private httpClient:HttpClient) { }

  //general announcements
  public getAnnouncements():Observable<Announcement[]>{
    return this.httpClient.get<Announcement[]>(`${this.REST_API_SERVER}/Announcements`);
  }
  //class announcements
}
