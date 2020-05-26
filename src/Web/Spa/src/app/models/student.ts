import { Program } from './program';
import { Observable } from 'rxjs';

export class Student {
    id: number;  
    pcn: string;
    firstName: string;
    lastName: string;
    initials:string;
    email:string;
    phoneNumber:string;
    accountStatus:string;
    dateOfBirth:Date;
    nationality:string;
    streetName:string;
    houseNumber:number;
    postalCode:string;
    city:string;
    country:string;
    program:Program;
    
  }
