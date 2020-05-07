import { Component, OnInit } from '@angular/core';
import { Email } from 'src/app/models/email';

@Component({
  selector: 'app-emails',
  templateUrl: './emails.component.html',
  styleUrls: ['./emails.component.css']
})
export class EmailsComponent implements OnInit {

  emails:Email[]=[
    new Email("thanhma@student.fontys.nl","15.30, May 02, 2020","Lockdown situation and Proep working progress",false),
    new Email("boris@student.fontys.nl","08.30, May 03, 2020","Lockdown situation and Domino's pizza discount",true),
    new Email("dungluong@student.fontys.nl","10.30, May 03, 2020","Lockdown situation and Weibo",false)
  ];

  constructor() { }

  ngOnInit(): void {
  }

  getEmailStatusColor(status:boolean):string{
    if(!status){
      return "text-primary";
    }else{
      return "text-muted";
    }
  }
  
}
