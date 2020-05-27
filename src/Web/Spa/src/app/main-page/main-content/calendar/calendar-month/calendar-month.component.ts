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
    this.GetFirstDaysAndWeekNumber();
  }

  GetFirstDaysAndWeekNumber(){
    let allFirstDaysOfWeekInMonth = [];
    let isoWeekNumber = [];
    let firstIsoWeekInMonth = moment().startOf('month').add(2,'M').isoWeek();
    let lastIsoweekInMonth = moment().endOf('month').add(2,'M').isoWeek();
    for(let i = firstIsoWeekInMonth; i <= lastIsoweekInMonth; i++) {
      allFirstDaysOfWeekInMonth.push(moment().isoWeek(i).startOf('isoWeek'));
      isoWeekNumber.push(i);
    }

    return {
      allFirstDays: allFirstDaysOfWeekInMonth,
      weekNumber: isoWeekNumber
    };
  }




}
