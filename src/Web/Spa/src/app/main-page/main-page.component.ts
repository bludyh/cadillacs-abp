import { Component, OnInit } from '@angular/core';
import { Student } from '../models/student';
import { IdentityService } from '../services/identity.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
  providers:[IdentityService]
})
export class MainPageComponent implements OnInit {

  student : Student = null;
  mouseOverNotification:boolean=false;

  activeFuncs:boolean[]=[true,false,false,false,false,false];

  constructor(private identityStudentsService:IdentityService,private route:ActivatedRoute) {
    
   }

  ngOnInit(): void {
    this.getStudent(1000033);//the student ID will be assigned after authentication
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

  mouseOverNotificationEvent(){
    this.mouseOverNotification=true;
  }

  mouseOutNotificationEvent(){
    this.mouseOverNotification=false;
  }

  getClassForMainContent():string{
    if(this.mouseOverNotification){
      return "col-md-9";
    }else{
      return "col-md-10";
    }
  }

  getClassForNotificationBar():string{
    if(this.mouseOverNotification){
      return "col-md-2 p-0 bg-light";
    }else{
      return "col-md-1 p-0 bg-light";
    }
  }
}
