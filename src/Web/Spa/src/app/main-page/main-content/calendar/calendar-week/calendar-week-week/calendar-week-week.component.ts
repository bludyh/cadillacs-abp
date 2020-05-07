import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import * as moment from 'moment';
import {Moment} from 'moment';
import {CalendarWeek} from '../../../../../models/CalendarWeek';

@Component({
  selector: 'app-calendar-week-week',
  templateUrl: './calendar-week-week.component.html',
  styleUrls: ['./calendar-week-week.component.css']
})
export class CalendarWeekWeekComponent implements OnChanges {
  @Input() WeekDiff: number;
  WeekObject: CalendarWeek;
  hours: Array<number> = Array.from(Array(9).keys()).map(h=>h+8);
  public ConvertFromWeekDifferent() {
    return moment().startOf('isoWeek').add(this.WeekDiff * 7, 'days' );
  }

  constructor() {
  }


  ngOnChanges(changes: SimpleChanges): void {
    this.WeekObject = new CalendarWeek(this.ConvertFromWeekDifferent());
  }

}
