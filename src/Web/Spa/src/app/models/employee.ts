import { School } from './school';
import { Room } from './room';

export class Employee{
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

export class ShortEmployee{
    id:number;
    firstName:string;
    lastName:string;
    initials:string;
}