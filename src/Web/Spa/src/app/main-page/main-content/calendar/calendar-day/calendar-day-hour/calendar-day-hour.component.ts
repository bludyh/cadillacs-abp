import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-calendar-day-hour',
  templateUrl: './calendar-day-hour.component.html',
  styleUrls: ['./calendar-day-hour.component.css']
})
export class CalendarDayHourComponent implements OnInit {
  @Input() hour: number;

  constructor() { }

  ngOnInit(): void {
  }

}
