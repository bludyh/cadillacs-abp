import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/models/student';
import { CadillacsServicesService } from 'src/app/cadillacs-services.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css'],
  providers:[CadillacsServicesService]
})
export class NavigationBarComponent implements OnInit {

  constructor(private cadillacsServices:CadillacsServicesService) { }
  student : Student = null;
   ngOnInit(): void {
     let students:Student[];
    //  this.cadillacsServices.getStudent(1000030).subscribe(studs=>{
    //     students=studs;
    //  });
    //  if(students.length>0){
    //    console.log(students);
    //    this.student=students[0];
    //  }
   }
 
   getUserName() : string {
       //return this.student.FirstName + this.student.LastName
       return "Testing Student";
   }


}
