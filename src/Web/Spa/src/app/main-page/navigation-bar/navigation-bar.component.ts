import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Student } from 'src/app/models/student';


@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {
  @Output() setActiveFuncChange:EventEmitter<number>=new EventEmitter();
  constructor() {
    
   }
  @Input() student : Student = null;
   ngOnInit(): void {
   }
   getUserName() : string {
     if(this.student!=null){
      return this.student.firstName+" "+this.student.initials;
     }
     return "";
   }

   activateFunc(){
    this.setActiveFuncChange.emit(5);
  }

}
