import { Component, OnInit,Input } from '@angular/core';
import { myCourse } from 'src/app/models/myCourse';
import { Assignment } from 'src/app/models/assignment';
import {formatDate } from '@angular/common';

@Component({
  selector: 'app-submissions',
  templateUrl: './submissions.component.html',
  styleUrls: ['./submissions.component.css']
})
export class SubmissionsComponent implements OnInit {

  counter:number = 0;

  addAssignment(course:myCourse, newAssignemnt:Assignment)  {
    course.assignments.push(newAssignemnt);
  } 

 getAssignments(course:myCourse) {

    return course.assignments;
}
assignmentsCourse: Assignment[]= [];
assignmentsCourse1: Assignment[]= [];

 myCourse:myCourse = {
    Name: 'PRC1',
    Type:'Elective',
    Description:'Something about something',
    date: new Date,
    assignments: this.assignmentsCourse
    
  }
  myCourse1:myCourse = {
    Name: 'PROEP',
    Type:'Project',
    Description:'Something about something',
    date: new Date,
    assignments: this.assignmentsCourse1
  }
  public date:Date;

  assignment= new Assignment("Animal Shelter",this.date,this.myCourse.Name,"For this assignemnt must must be able to show competence in c code");
  assignment1= new Assignment("Big project",this.date,this.myCourse1.Name,"For this assignemnt must must be able to show competence in c code");

 
  myCourses:myCourse[] = [this.myCourse, this.myCourse1]


  CourseName:string = 'PRC';
  CourseAssignemnt:string ='PRC assignemnt';

  @Input() isActive:boolean=true;
  today= new Date();
  jstoday = '';
  constructor() {
    this.jstoday = formatDate(this.today, 'd-MMMM-yyyy', 'en-US', '+0530');
  }

  ngOnInit(): void {
    console.log("onInit ")


    this.addAssignment(this.myCourse1, this.assignment);
    this.addAssignment(this.myCourse1, this.assignment1);


  }

  
  // setColorScheme(){
  //   let myCourses = {
  //     courseElective: this.myCourse.Type == 'Elective',
  //     courseProject: this.myCourse.Type == 'Project'
  //   }
  //   return myCourses;
  // }

}
