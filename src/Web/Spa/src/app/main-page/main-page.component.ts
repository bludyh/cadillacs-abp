import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  activeFuncs:boolean[]=[true,false,false,false,false];
  constructor() { }

  ngOnInit(): void {
  }

  activateFunction(index:number){
    for(let i=0;i<this.activeFuncs.length;i++){
      if(i===index){
        this.activeFuncs[i]=true;
      }else{
        this.activeFuncs[i]=false;
      }
    }
  }
}
