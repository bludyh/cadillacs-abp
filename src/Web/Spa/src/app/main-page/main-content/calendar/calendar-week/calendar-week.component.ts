import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import * as moment from 'moment';
import {CalendarWeek} from '../../../../models/CalendarWeek';

@Component({
  selector: 'app-calendar-week',
  templateUrl: './calendar-week.component.html',
  styleUrls: ['./calendar-week.component.css']
})
export class CalendarWeekComponent implements OnInit {
  @Input() weekDifferent: number;

  constructor() { }

  ngOnInit(): void {
  }

}
