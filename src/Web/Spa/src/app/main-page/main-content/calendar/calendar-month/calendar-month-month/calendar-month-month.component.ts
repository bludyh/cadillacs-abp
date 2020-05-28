import {Component, DoCheck, Input, OnChanges, OnInit, SimpleChange, SimpleChanges} from '@angular/core';
import {Moment} from "moment";
import {CalendarSubjectObject} from "../../../../../models/calendarSubjectObject";
import {WeekDayAndWeekNumber} from "../../../../../models/WeekDayAndWeekNumber";

@Component({
  selector: 'app-calendar-month-month',
  templateUrl: './calendar-month-month.component.html',
  styleUrls: ['./calendar-month-month.component.css']
})
export class CalendarMonthMonthComponent implements OnInit {

  @Input() allFirstDayOnWeeks = [];
  @Input() allWeekNumbers = [];
  //array of arrays
  @Input() daysInMonthAndWeekNumber:Array<WeekDayAndWeekNumber> = [];

  constructor() {

  }

  subjects: Array<CalendarSubjectObject> = [
    new CalendarSubjectObject('CSA', '27-04-2020', '08:45 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('OS1', '27-04-2020', '10:30 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('ProCP', '01-05-2020', '12:45 PM', 'Emin Thaqi'),
    new CalendarSubjectObject('Web3', '27-04-2020', '02:30 PM', 'Mikaeil Shaghelani'),
    new CalendarSubjectObject('CSA', '28-04-2020', '08:45 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('Web3', '28-04-2020', '02:30 PM', 'Mikaeil Shaghelani'),
    new CalendarSubjectObject('CSA', '29-04-2020', '08:45 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('OS1', '29-04-2020', '10:30 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('Web3', '30-04-2020', '02:30 PM', 'Mikaeil Shaghelani')
  ];

  ngOnInit(): void {

  }


  getDateSchedule(date:string):CalendarSubjectObject[]{
    let output:CalendarSubjectObject[]=[];
    this.subjects.forEach(element => {
      if(element.Date===date){
        output.push(element);
      }
    });
    return output;
  }

}
