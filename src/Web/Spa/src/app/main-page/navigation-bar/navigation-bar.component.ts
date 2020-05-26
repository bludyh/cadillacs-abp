import { Component, OnInit, Input } from '@angular/core';
import { Student } from 'src/app/models/student';


@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {

  constructor() {
    
   }
  @Input() student : Student = null;
   ngOnInit(): void {
   }
   getUserName() : string {
       return this.student.firstName+" "+this.student.lastName[0];
   }


}
