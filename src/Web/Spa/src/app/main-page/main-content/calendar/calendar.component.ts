import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ScheduleService} from "../../../services/schedule.service";
import {Class} from "../../../models/class";
import {Schedule} from "../../../models/schedule";
import {Room} from "../../../models/room";
import {Observable} from "rxjs";
import {Course} from "../../../models/course";
import {Building} from "../../../models/building";
import {buildDriverProvider} from "protractor/built/driverProviders";

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  activeCalendarStatus: Array<boolean> = [true, false, false];
  weekChangeValue = 0;
  dayChangeValue = 0;

  schedulesTest:Schedule[];

  setActivePage(index: number) {
    for (let i = 0; i < this.activeCalendarStatus.length; i++) {
      this.activeCalendarStatus[i] = i === index;
    }
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

  constructor(private scheduleService:ScheduleService) {

  }

  getSchedule(){
    this.scheduleService.getSchedules("prc1","e-s71",1,2020).subscribe(
      (returnedSchedules:Schedule[])=>{
        this.schedulesTest = returnedSchedules;
      }
    );
  }

  postSchedule(){
    let course: Course = {id:'12',credit:2,description:'abc',name:'ALE'};
    let testClass:Class = {id:'1',semester:1,year:2020, course:course,courseID:course.id,courseName:course.name,startDate:new Date(),endDate:new Date()};
    let building:Building = {id: '1'};
    let room:Room = {id:'12',building:building};
    this.scheduleService.postSchedule(testClass,1,testClass.startDate,room);
  }

  showScheduleTest(){
    console.log(this.schedulesTest);
  }

  ngOnInit(): void {
    this.getSchedule();
  }
}
