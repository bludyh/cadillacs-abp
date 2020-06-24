import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/models/course';
import { Student } from 'src/app/models/student';
import { CourseService } from 'src/app/services/course.service';
import { Enrollment } from 'src/app/models/enrollment';

@Component({
  selector: 'app-enroll-course',
  templateUrl: './enroll-course.component.html',
  styleUrls: ['./enroll-course.component.css'],
  providers:[CourseService]
})
export class EnrollCourseComponent implements OnInit {

  @Input() courses:Course[]=[];

  availableCourses:Course[]=[];

  constructor(private courseService:CourseService) { }

  ngOnInit(): void {
    this.getCourses();
  }

  getCourses(){
    this.courseService.getCourses().subscribe(
      (courses:Course[]) => {
        // Gets all courses that the student is not enrolled in.
        this.availableCourses = courses.filter(c => !this.courses.find(({ id }) => c.id === id));
      }
    )
  }
}
