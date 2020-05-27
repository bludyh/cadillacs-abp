import { Component, OnInit, Input } from '@angular/core';
import { Announcement } from 'src/app/models/announcement';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {

  @Input() announcement:Announcement;

  isTruncated:boolean=true;

  constructor() { }

  ngOnInit(): void {
  }

  switchTruncate(){
    this.isTruncated=!this.isTruncated;
  }

  getTruncateClass():string{
    if(this.isTruncated){
      return "card-text m-0 text-truncate";
    }else{
      return "card-text m-0";
    }
  }

  getReadButtonStatus(){
    if(this.isTruncated){
      return "more";
    }else{
      return "less";
    }
  }
}
