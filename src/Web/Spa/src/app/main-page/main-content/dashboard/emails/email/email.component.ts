import { Component, OnInit, Input } from '@angular/core';
import { Email } from 'src/app/models/email';

@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.css']
})
export class EmailComponent implements OnInit {

  @Input() email:Email=null;
  constructor() { }

  ngOnInit(): void {
  }

}
