import { Component, OnInit, Input } from '@angular/core';
import { Student } from 'src/app/models/student';
import { IdentityService } from 'src/app/services/identity.service';
import { TeacherMentor } from 'src/app/models/mentor';

@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent implements OnInit {

  @Input() student:Student=null;

  @Input() isActive:boolean=true;

  mentor:TeacherMentor

  constructor(private identityService:IdentityService) { }

  ngOnInit(): void {
   this.getStudyMentor();
   
  }

  getStudyMentor(){
    this.identityService.getStudentMentors(this.student.id).subscribe(
      (mentors:TeacherMentor[])=>{
        mentors.forEach(m => {
          let mentorType:string=m.mentorType;
          if(mentorType=="STUDY"){
            this.mentor=m;
          }
        });
      }
    )
  }
}
