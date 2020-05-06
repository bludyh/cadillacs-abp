import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/models/course';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {

  courses:Course[]=[
    new Course("PCS1","Programming in C# - Course 1"),
    new Course("PCS2","Programming in C# - Course 2"),
    new Course("PCS3","Programming in C# - Course 3"),
    new Course("CSA","Client-Service Application in C#"),
    new Course("ASP","ASP.NET Core"),
  ];

  @Input() isActive:boolean=true;

  constructor() { }

  ngOnInit(): void {
  }

}
