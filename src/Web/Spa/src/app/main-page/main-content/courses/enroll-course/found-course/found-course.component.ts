import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/models/course';

@Component({
  selector: 'app-found-course',
  templateUrl: './found-course.component.html',
  styleUrls: ['./found-course.component.css']
})
export class FoundCourseComponent implements OnInit {

  @Input() course:Course=null;
  
  constructor() { }

  ngOnInit(): void {
  }

}
