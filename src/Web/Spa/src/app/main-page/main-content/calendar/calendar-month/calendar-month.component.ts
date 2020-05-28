import {Component, DoCheck, Input, OnChanges, OnInit} from '@angular/core';
import {CalendarSubjectObject} from '../../../../models/CalendarSubjectObject';
import { element } from 'protractor';
import * as moment from 'moment';
import {WeekDayAndWeekNumber} from "../../../../models/WeekDayAndWeekNumber";
import {Moment} from "moment";

@Component({
  selector: 'app-calendar-month',
  templateUrl: './calendar-month.component.html',
  styleUrls: ['./calendar-month.component.css']
})
export class CalendarMonthComponent implements OnChanges {

  constructor() { }

  @Input() monthDifferent = 0;
  allFirstDayOnWeeks = [];
  allWeekNumbers:Array<number> = [];
  allDaysInMonthAndWeekNumber = [];

  ngOnChanges(): void {
      this.allFirstDayOnWeeks = this.GetFirstDaysAndWeekNumber(this.monthDifferent).allFirstDays;
      this.allWeekNumbers = this.GetFirstDaysAndWeekNumber(this.monthDifferent).weekNumber;
      this.allDaysInMonthAndWeekNumber = this.GetDaysInMonth(this.allFirstDayOnWeeks, this.allWeekNumbers);
  }


  GetFirstDaysAndWeekNumber(different:number){
    let allFirstDaysOfWeekInMonth = [];
    let isoWeekNumber = [];
    let firstIsoWeekInMonth = moment().startOf('month').add(different,'M').isoWeek();
    let lastIsoweekInMonth = moment().endOf('month').add(different,'M').isoWeek();
    for(let i = firstIsoWeekInMonth; i <= lastIsoweekInMonth; i++) {
      allFirstDaysOfWeekInMonth.push(moment().isoWeek(i).startOf('isoWeek'));
      isoWeekNumber.push(i);
    }
    return {
      allFirstDays: allFirstDaysOfWeekInMonth,
      weekNumber: isoWeekNumber
    };
  }

  GetDaysInMonth(FirstDaysOnWeekArray:Array<Moment>, WeekNumberArray:Array<number>){
    let WeekDayAndWeekNumberObjects = [];
    let index = 0;
    FirstDaysOnWeekArray.forEach(function (value) {
      let daysInWeek = [];
      //add first day of week
      daysInWeek.push(value.format('DD-MM-YYYY'));
      //add the rest
      for (let i = 0; i < 4; i++){
        daysInWeek.push(value.add(1,'days').format('DD-MM-YYYY'));
      }
      WeekDayAndWeekNumberObjects.push(new WeekDayAndWeekNumber(daysInWeek,WeekNumberArray[index]));
      index++;
    });
    return WeekDayAndWeekNumberObjects;
  }

  // GetDaysInMonth(){
  //   let WeekDayAndWeekNumberObjects = [];
  //   let index = 0;
  //   this.allFirstDayOnWeeks.forEach(function (value) {
  //     let daysInWeek = [];
  //     //add first day of week
  //     daysInWeek.push(value.format('DD-MM-YYYY'));
  //     //add the rest
  //     for (let i = 0; i < 4; i++){
  //       daysInWeek.push(value.add(1,'days').format('DD-MM-YYYY'));
  //     }
  //     WeekDayAndWeekNumberObjects.push(daysInWeek);
  //     index++;
  //   });
  //   WeekDayAndWeekNumberObjects.forEach(function (value) {
  //     console.log(value);
  //   });
  //   return WeekDayAndWeekNumberObjects;
  // }




}
