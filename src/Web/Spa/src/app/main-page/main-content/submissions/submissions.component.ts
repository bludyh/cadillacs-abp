import { Component, OnInit,Input } from '@angular/core';

@Component({
  selector: 'app-submissions',
  templateUrl: './submissions.component.html',
  styleUrls: ['./submissions.component.css']
})
export class SubmissionsComponent implements OnInit {

  @Input() isActive:boolean=true;
  constructor() { }

  ngOnInit(): void {
  }

}
