import {Component, OnInit, Input, OnChanges} from '@angular/core';
import { CalendarSubjectObject } from 'src/app/models/CalendarSubjectObject';

@Component({
  selector: 'app-calendar-month-day',
  templateUrl: './calendar-month-day.component.html',
  styleUrls: ['./calendar-month-day.component.css']
})
export class CalendarMonthDayComponent implements OnInit {

  @Input() subjects:CalendarSubjectObject[]=[];

  constructor() { }

  ngOnInit(): void {

  }
  scheduleToggle(popover, subject: string, date: string, time: string, teacherName: string) {
    if (popover.isOpen()) {
      popover.close();
    } else {
      popover.open({subject, date, time, teacherName});
    }
  }

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
}
