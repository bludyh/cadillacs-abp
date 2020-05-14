import { Component, OnInit } from '@angular/core';
import {CalendarSubjectObject} from '../../../../models/CalendarSubjectObject';
import { element } from 'protractor';
import * as moment from 'moment';

@Component({
  selector: 'app-calendar-month',
  templateUrl: './calendar-month.component.html',
  styleUrls: ['./calendar-month.component.css']
})
export class CalendarMonthComponent implements OnInit {

  constructor() { }


  ngOnInit(): void {
  }

  testingFunction(){
    let firstIsoWeekInMonth = moment().startOf('month').isoWeek();
    let lastIsoweekInMonth = moment().endOf('month').isoWeek();
    let something = "";

    for(let i = firstIsoWeekInMonth; i <= lastIsoweekInMonth; i++) {
      something += i + " - " + moment().isoWeek(i).startOf('isoWeek').format('DD-MM-YYYY') + "___________";
    }
    return something;
  }




}
