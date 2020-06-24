import { Class } from './class';
import { AssignmentType } from './assignmentType';

export class Assignment{
    id: number;
    class: Class;
    name: string;
    type: AssignmentType;
    description: string;
    deadlineDateTime: Date;
    weight: number;

    // public DueTime?:Date;
    // public Name?:string;
    // public CourseName?:string;
    // public Description?:string;

    // public constructor(name:string, dueTime:Date,courseName:string,description:string){
    //     this.DueTime=dueTime;
    //     this.Name=name;
    //     this.CourseName=courseName;
    //     this.Description=description;
    // }
}