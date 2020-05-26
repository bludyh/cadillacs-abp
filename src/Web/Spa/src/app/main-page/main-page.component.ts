import { Component, OnInit } from '@angular/core';
import { Student } from '../models/student';
import { IdentityService } from '../identity.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
  providers:[IdentityService]
})
export class MainPageComponent implements OnInit {

  student : Student = null;

  activeFuncs:boolean[]=[true,false,false,false,false];

  constructor(private identityStudentsService:IdentityService) {
    
   }

  ngOnInit(): void {
    this.getStudent(1000030);
  }

  activateFunction(index:number){
    for(let i=0;i<this.activeFuncs.length;i++){
      if(i===index){
        this.activeFuncs[i]=true;
      }else{
        this.activeFuncs[i]=false;
      }
    }
  }

  getStudent(studentID:number){
    this.identityStudentsService.getStudent(studentID).subscribe(
      (student:Student)=>{
        this.student=student;
      }
    )
  }
}
