import { Component, OnInit } from '@angular/core';
import { Activity } from 'src/app/models/activity';

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.css']
})
export class ActivitiesComponent implements OnInit {

  activities:Activity[]=[
    new Activity("13.00","PROEP - Presentation")
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
