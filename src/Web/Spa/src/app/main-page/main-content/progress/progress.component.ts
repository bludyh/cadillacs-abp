import { Component, OnInit,Input } from '@angular/core';
import { Progress } from 'src/app/models/progress';

@Component({
  selector: 'app-progress',
  templateUrl: './progress.component.html',
  styleUrls: ['./progress.component.css']
})
export class ProgressComponent implements OnInit {

  progresses:Progress[]=[
    new Progress(60,60,"Propedeutic phase"),
    new Progress(108,120,"Core phase"),
    new Progress(3,60,"Graduation phase"),
  ];

  @Input() isActive:boolean=true;
  constructor() { }

  ngOnInit(): void {
  }

}
