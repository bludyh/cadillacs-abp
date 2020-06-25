import { School } from './school';
import { Room } from './room';
import { Building } from './building';

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
    building:Building;
}