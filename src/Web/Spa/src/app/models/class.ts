import { Course } from './course';

export class Class{
    id:string;
    semester:number;
    year:number;
    startDate:Date;
    endDate:Date;
    course:Course;
}

export class ShortClass{
    id:string;
    semester:number;
    year:number;
    courseID:string;
    courseName:string;
}