import { Component, OnInit, Input } from '@angular/core';
import { Announcement } from 'src/app/models/announcement';
import { AnnouncementService } from 'src/app/services/announcement.service';

@Component({
  selector: 'app-notification-bar',
  templateUrl: './notification-bar.component.html',
  styleUrls: ['./notification-bar.component.css']
})
export class NotificationBarComponent implements OnInit {

  @Input() isExpanded:boolean;
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



  clickTest(){
    let announcement:Announcement={id:999,title:"test post",body:"test",dateTime:new Date("2020-17-02"),employee:null};
    this.announcementService.postAnnoucement(announcement);
  }
}
