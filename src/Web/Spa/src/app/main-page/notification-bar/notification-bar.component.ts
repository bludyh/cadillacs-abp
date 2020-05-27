import { Component, OnInit } from '@angular/core';
import { Announcement } from 'src/app/models/announcement';
import { AnnouncementService } from 'src/app/announcement.service';

@Component({
  selector: 'app-notification-bar',
  templateUrl: './notification-bar.component.html',
  styleUrls: ['./notification-bar.component.css']
})
export class NotificationBarComponent implements OnInit {

  announcements:Announcement[]=[];

  constructor(private announcementService:AnnouncementService) { }

  ngOnInit(): void {
    this.getAnnouncements();
  }

  getAnnouncements(){
    this.announcementService.getAnnouncements().subscribe(
      (announces:Announcement[])=>{
        this.announcements=announces;
      }
    )
  }
}
