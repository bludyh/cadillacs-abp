import { Component, OnInit, Input, Output,EventEmitter  } from '@angular/core';
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
  @Output() enrollDoneEvent=new EventEmitter<void>();
  classesBasket:Class[]=[];

  availableCourses:Course[]=[];
  availableClassesOfCourses:Class[]=[];

  constructor(private courseService:CourseService) { 
    
  }

  ngOnInit(): void {
    this.getCourses();
  }


  getCourses(){
    this.courseService.getCourses().subscribe(
      (inCourses:Course[]) => {
        let courses:Course[]=this.courses;
        // Gets all courses that the student is not enrolled in.
        this.availableCourses = inCourses.filter(c => !courses.find(({ id }) => c.id === id));
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

  addClass($event){
    this.classesBasket.push($event);
    console.log(this.classesBasket);
  }
  
  removeClass($event){
    let index:number=this.classesBasket.indexOf($event);
    this.classesBasket.splice(index,1);
    console.log(this.classesBasket);
  }

  enrollClass(){
    let classes:Class[]=this.classesBasket;
    classes.forEach(ec => {
      console.log(ec);
      this.courseService.postEnrollment(ec.course.id,ec.id,ec.semester,ec.year,1000033);
    });
    this.classesBasket=[];
    this.enrollDoneEvent.emit();
  }
}
