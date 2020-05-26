import { Component, OnInit, Input, DebugElement } from '@angular/core';
import { Progress } from 'src/app/models/progress';

@Component({
  selector: 'app-progress-object',
  templateUrl: './progress-object.component.html',
  styleUrls: ['./progress-object.component.css']
})
export class ProgressObjectComponent implements OnInit {

@Input() progress:Progress=null;


  constructor() { }

  ngOnInit(): void {
  }


  progressColor(percent:number):string{
    let color:string="bg-secondary";
    if(percent>0&&percent<50){
      color="bg-danger";
    }
    else if(percent<75){
      color="bg-warning";
    }
    else if(percent<99){
      color="bg-primary";
    }
    else{
      color="bg-success";
    }
    return color;
  }

}
