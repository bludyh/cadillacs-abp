import { ShortEmployee } from './employee';
import { Class } from './class';


export class Announcement{
    id:number;
    title:string;
    body:string;
    dateTime:Date;
    employee:ShortEmployee;
}

export class ClassAnnouncement{
    announcement:Announcement;
    class:Class;
}