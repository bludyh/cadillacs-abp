import { Component, OnInit, Input, Output,EventEmitter } from '@angular/core';
import { Course } from 'src/app/models/course';
import { Class } from 'src/app/models/class';

@Component({
  selector: 'app-found-course',
  templateUrl: './found-course.component.html',
  styleUrls: ['./found-course.component.css']
})
export class FoundCourseComponent implements OnInit {

  @Input() classOfCourse:Class;
  @Output() addClassEvent=new EventEmitter<Class>();
  @Output() removeClassEvent=new EventEmitter<Class>();
  course:Course=null;
  isChecked:Boolean=false;

  constructor() { }

  ngOnInit(): void {
    
  }

  addClass(){
    this.addClassEvent.emit(this.classOfCourse);
  }

  removeClass(){
    this.removeClassEvent.emit(this.classOfCourse);
  }

  checkedChange(){
    this.setChecked(!this.isChecked);
  }

  setChecked(value:boolean){
    if(value==true){
      this.addClass();
    }else{
      this.removeClass();
    }
    this.isChecked=value;
  }
}
