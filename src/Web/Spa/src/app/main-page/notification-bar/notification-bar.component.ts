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
}
