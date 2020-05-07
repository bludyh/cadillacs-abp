import { Component, OnInit, Input } from '@angular/core';
import { Activity } from 'src/app/models/activity';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
export class ActivityComponent implements OnInit {

  @Input() activity:Activity=null;

  constructor() { }

  ngOnInit(): void {
  }

}
