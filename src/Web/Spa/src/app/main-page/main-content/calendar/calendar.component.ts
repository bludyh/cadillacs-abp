import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import { StudyProgressService } from 'src/app/services/study-progress.service';
import { ScheduleService } from 'src/app/services/schedule.service';
import { Enrollment } from 'src/app/models/enrollment';
import { Class } from 'src/app/models/class';
import { Schedule } from 'src/app/models/schedule';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  activeCalendarStatus: Array<boolean> = [true, false, false];
  weekChangeValue = 0;
  dayChangeValue = 0;
  enrollments:Enrollment[]=[];

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

  constructor(private studyProgressService:StudyProgressService, private scheduleService:ScheduleService) { }

  ngOnInit(): void {
    this.getEnrollments(1000033);
    
    
  }
  
  clickTest(){
    let enrolls:Enrollment[]=this.enrollments;
    let activeClasses:Class[]=this.getActiveClasses(enrolls);
    let schedules:Schedule[]=this.getSchedules(activeClasses);
    console.log(schedules);
  }

  getEnrollments(studentID:number){
    this.studyProgressService.getEnrollments(studentID).subscribe(
      (enrolls:Enrollment[])=>{
        this.enrollments=enrolls;
      }
    )
  }

  getActiveClasses(enrolls:Enrollment[]):Class[]{
    let classes:Class[]=[];
    enrolls.forEach(enr => {
      let enrDate:Date=new Date(enr.class.endDate);
      let curDate:Date=new Date();
      if(enrDate>=curDate){
        classes.push(enr.class);
      }
    });
    return classes;
  }

  getSchedules(classes:Class[]):Schedule[]{
    let schedules:Schedule[]=[];
    classes.forEach(cl => {
      this.scheduleService.getSchedules(cl.course.id,cl.id,cl.semester,cl.year).subscribe(
        (sc:Schedule[]) => {
          sc.forEach(s => {
            schedules.push(s);
          });
        }
      )
    })
    return schedules;
  } 

}
