import { Component, OnInit, Input } from '@angular/core';
import { Student } from 'src/app/models/student';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.css']
})
export class MainContentComponent implements OnInit {

  @Input() student : Student = null;
  
  @Input() activeFunctions:boolean[]=[false,false,false,false,false,false];

  constructor() { }

  ngOnInit(): void {
  }

}
