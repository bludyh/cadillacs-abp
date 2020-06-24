import { Component, OnInit,Input } from '@angular/core';
import { Progress } from 'src/app/models/progress';
import { Enrollment } from 'src/app/models/enrollment';
import { StudyProgressService } from 'src/app/services/study-progress.service';
import { Student } from 'src/app/models/student';

@Component({
  selector: 'app-progress',
  templateUrl: './progress.component.html',
  styleUrls: ['./progress.component.css']
})
export class ProgressComponent implements OnInit {

  @Input() student:Student=null;
  enrollments:Enrollment[]=[];

  progresses:Progress[]=[
    new Progress(0,60,"Propedeutic phase"),
    new Progress(0,120,"Core phase"),
    new Progress(0,60,"Graduation phase"),
  ];

  @Input() isActive:boolean=true;
  constructor(private studyProgressService:StudyProgressService) { }

  ngOnInit(): void {
    this.getEnrollments(this.student.id);
    this.getProgresses();
  }

  getEnrollments(studentID:number){
    this.studyProgressService.getEnrollments(studentID).subscribe(
      (enrolls:Enrollment[])=>{
        this.enrollments=enrolls;
      }
    )
  }

  getProgresses(){
    if(this.enrollments.length>0){
      this.enrollments.forEach(enrollment => {
        let semester:number=enrollment.class.semester;
        let credit:number=enrollment.class.course.credit;
        let grade:number=enrollment.finalGrade;
        if(grade>=5.5){
          this.addEC(semester,credit);
        }
      });
    }
  }

  addEC(semester:number,credit:number){
    switch(semester){
      case 1||2:this.progresses[0].addEC(credit);return;
      case 3||4||5||6:this.progresses[1].addEC(credit);return;
      case 7||8:this.progresses[2].addEC(credit);return;
      default:return;
    }
  }

}
