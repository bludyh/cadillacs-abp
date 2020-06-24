import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/models/course';
import { Student } from 'src/app/models/student';
import { CourseService } from 'src/app/services/course.service';
import { Enrollment } from 'src/app/models/enrollment';
import { Class } from 'src/app/models/class';

@Component({
  selector: 'app-enroll-course',
  templateUrl: './enroll-course.component.html',
  styleUrls: ['./enroll-course.component.css'],
  providers:[CourseService]
})
export class EnrollCourseComponent implements OnInit {

  @Input() courses:Course[]=[];

  availableCourses:Course[]=[];
  availableClassesOfCourses:Class[]=[];

  constructor(private courseService:CourseService) { 
    this.getCourses();
  }

  ngOnInit(): void {
    
  }

  getCourses(){
    this.courseService.getCourses().subscribe(
      (inCourses:Course[]) => {
        console.log(this.courses);
        // Gets all courses that the student is not enrolled in.
        this.availableCourses = inCourses.filter(c => !this.courses.find(({ id }) => c.id === id));
        console.log(this.availableCourses);
        //call getClasses
        this.getClassesInCourse(this.availableCourses);
      }
    )
  }

  getClassesInCourse(avaiCourses:Course[]){
    avaiCourses.forEach(course => {
      this.courseService.getClassesInCourse(course.id).subscribe(
        (classes:Class[])=>{
          classes.forEach(c => {
            this.availableClassesOfCourses.push(c);
          });
        }
      )
    });
  }
}
