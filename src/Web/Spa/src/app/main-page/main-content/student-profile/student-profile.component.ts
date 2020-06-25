import { Component, OnInit, Input } from '@angular/core';
import { Student } from 'src/app/models/student';

@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent implements OnInit {

  @Input() student:Student=null;

  @Input() isActive:boolean=true;

  constructor() { }

  ngOnInit(): void {
  }

}
