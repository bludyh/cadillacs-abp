import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/models/course';
import { Student } from 'src/app/models/student';
import { StudyProgressService } from 'src/app/study-progress.service';
import { Enrollment } from 'src/app/models/enrollment';

@Component({
  selector: 'app-enroll-course',
  templateUrl: './enroll-course.component.html',
  styleUrls: ['./enroll-course.component.css']
})
export class EnrollCourseComponent implements OnInit {

  @Input() courses:Course[]=[];
  constructor() { }

  ngOnInit(): void {
  }



}
