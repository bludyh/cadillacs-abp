import { Component, OnInit,Input } from '@angular/core';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  @Input() isActive:boolean=true;
  constructor() { }

  ngOnInit(): void {
  }

}
