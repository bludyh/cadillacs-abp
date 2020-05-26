import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-calendar-week-day',
  templateUrl: './calendar-week-day.component.html',
  styleUrls: ['./calendar-week-day.component.css']
})
export class CalendarWeekDayComponent implements OnInit {
  @Input() day: string = '';
  @Input() isFirstdayOfWeek: boolean = false;
  hours: Array<number> = Array.from(Array(9).keys()).map(h=>h+8);

  constructor() { }

  ngOnInit(): void {
  }

}
