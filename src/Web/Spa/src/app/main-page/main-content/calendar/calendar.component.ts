import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  activeCalendarStatus: Array<boolean> = [true, false, false];
  weekChangeValue = 0;
  dayChangeValue = 0;
  monthChangeValue = 0;

  setActivePage(index: number) {
    for (let i = 0; i < this.activeCalendarStatus.length; i++) {
      this.activeCalendarStatus[i] = i === index;
    }
  }

  ChangeCurrentMonth(UpOrDown:boolean){
    if (UpOrDown) {
      this.monthChangeValue += 1;
    } else {
      this.monthChangeValue -= 1;
    }
  }

  ResetMonthChangeValue() {
    this.monthChangeValue = 0;
  }

  ChangeCurrentWeek(UpOrDown: boolean) {
    if (UpOrDown) {
      this.weekChangeValue += 1;
    } else {
      this.weekChangeValue -= 1;
    }
  }

  ResetWeekChangeValue() {
    this.weekChangeValue = 0;
  }


  ChangeCurrentDay(UpOrDown: boolean) {
    if (UpOrDown) {
      this.dayChangeValue += 1;
    } else {
      this.dayChangeValue -= 1;
    }
  }

  ResetDayChangeValue() {
    this.dayChangeValue = 0;
  }

  constructor() { }

  ngOnInit(): void {
    this.weekChangeValue = 0;
    this.dayChangeValue = 0;
    this.monthChangeValue = 0;
  }
}
