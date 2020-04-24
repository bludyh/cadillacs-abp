import { Component, OnInit } from '@angular/core';
import { Student } from '../student';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {

  constructor() { }
  student : Student = {PCN:123321, FirstName: 'Bobby',LastName: 'Gramatikov'}
   ngOnInit(): void {
   }
 
   getUserName() : string {
       return this.student.FirstName + this.student.LastName
   }

}
