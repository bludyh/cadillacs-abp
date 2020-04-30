import { Component, OnInit, Output,EventEmitter } from '@angular/core';
import {Function} from 'src/app/models/function';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {

  @Output() setActiveFuncChange:EventEmitter<number>=new EventEmitter();

  functionalities:string[]=["Dashboard","Courses","Submissions","Progress","Calendar"];
  active_function:string="";

  functions:Function[]=[
    new Function("fas fa-chart-pie","Dashboard"),
    new Function("fas fa-book","Courses"),
    new Function("fas fa-cloud-upload-alt","Submissions"),
    new Function("fas fa-chart-line","Progress"),
    new Function("far fa-calendar-alt","Calendar"),
  ];

  constructor() { }

  ngOnInit(): void {
  }

  isActive(func:string):boolean{
    return this.active_function===func;
  }

  activateFunc(index:number){
    this.setActiveFuncChange.emit(index);
  }

}
