import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/models/course';
import { Student } from 'src/app/models/student';
import { StudyProgressService } from 'src/app/services/study-progress.service';
import { Enrollment } from 'src/app/models/enrollment';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css'],
  providers:[StudyProgressService]
})
export class CoursesComponent implements OnInit {

  @Input() student:Student=null;

  @Input() isActive:boolean=true;

  enrollments:Enrollment[]=[];
  courses:Course[]=[];
  

  constructor(private studyProgressService:StudyProgressService) { }

  ngOnInit(): void {
    this.getEnrollments(this.student.id);
    //this.getEnrollments(this.student.id);
  }

  getEnrollments(studentID:number){
    this.studyProgressService.getEnrollments(studentID).subscribe(
      (enrolls:Enrollment[])=>{
        this.enrollments=enrolls;
        this.getCourses(enrolls);
      }
    )
  }

  getCourses(enrolls:Enrollment[]){
    enrolls.forEach(enrollment => {
      this.courses.push(enrollment.class.course);
    });
    
  }

  refreshEnrolledCourses(){
    this.enrollments=[];
    this.getEnrollments(this.student.id);
  }
}
