import { School } from './school';
import { Room } from './room';

export class Teacher {
    id:number;
    pcn:string;
    firstName:string;
    lastName:string;
    initials:string;
    email:string;
    phoneNumber:string;
    accountStatus:string;
    school:School;
    room:Room;
  }
