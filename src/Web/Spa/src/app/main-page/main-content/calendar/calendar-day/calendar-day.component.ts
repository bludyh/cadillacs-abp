import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-calendar-day',
  templateUrl: './calendar-day.component.html',
  styleUrls: ['./calendar-day.component.css']
})

export class CalendarDayComponent implements OnChanges {

  hours: Array<number> = Array.from(Array(9).keys()).map(h=>h+8);
  @Input() dayChange = 0;
  dayValue: string;

  ngOnChanges(changes: SimpleChanges): void {
    this.dayValue = moment().add(this.dayChange, 'days').format('MMM DD');
  }

}

