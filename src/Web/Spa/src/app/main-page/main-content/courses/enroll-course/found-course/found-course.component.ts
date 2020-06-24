import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/models/course';
import { Class } from 'src/app/models/class';

@Component({
  selector: 'app-found-course',
  templateUrl: './found-course.component.html',
  styleUrls: ['./found-course.component.css']
})
export class FoundCourseComponent implements OnInit {

  @Input() classOfCourse:Class;
  course:Course=null;
  
  constructor() { }

  ngOnInit(): void {
    
  }

}
