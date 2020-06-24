import { School } from './school';
import { Room } from './room';
import { Building } from './building';

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
    building:Building;
}

export class ShortEmployee{
    id:number;
    firstName:string;
    lastName:string;
    initials:string;
}