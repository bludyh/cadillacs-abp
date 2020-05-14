import { Component, OnInit } from '@angular/core';
import {CalendarSubjectObject} from "../../../../../models/calendarSubjectObject";

@Component({
  selector: 'app-calendar-month-week',
  templateUrl: './calendar-month-week.component.html',
  styleUrls: ['./calendar-month-week.component.css']
})
export class CalendarMonthWeekComponent implements OnInit {

  showVar: boolean = false;

  dates:string[]=[
    '31/12/2019',
    '01/01/2020',
    '02/01/2020',
    '03/01/2020',
    '04/01/2020'
  ];

  subjects: Array<CalendarSubjectObject> = [
    new CalendarSubjectObject('CSA', '31/12/2019', '08:45 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('OS1', '31/12/2019', '10:30 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('ProCP', '31/12/2019', '12:45 PM', 'Emin Thaqi'),
    new CalendarSubjectObject('Web3', '01/01/2020', '02:30 PM', 'Mikaeil Shaghelani'),
    new CalendarSubjectObject('CSA', '02/01/2020', '08:45 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('Web3', '02/01/2020', '02:30 PM', 'Mikaeil Shaghelani'),
    new CalendarSubjectObject('CSA', '03/01/2020', '08:45 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('OS1', '03/01/2020', '10:30 AM', 'Jaap Geurts'),
    new CalendarSubjectObject('Web3', '04/01/2020', '02:30 PM', 'Mikaeil Shaghelani')
  ];

  toggleComponent() {
    this.showVar = !this.showVar;
  }

  constructor() { }


  colourTimeSlot(timeSlot: string): string {
    let color = 'btn-secondary';
    if (timeSlot === '08:45 AM') {
      color = 'btn-primary';
    } else if (timeSlot === '10:30 AM') {
      color = 'btn-danger';
    } else if (timeSlot === '12:45 PM') {
      color = 'btn-warning';
    } else {
      color = 'btn-success';
    }
    return color;
  }

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
