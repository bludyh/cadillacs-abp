import { Student } from './student';
import { Class } from './class';
import { Group } from './group';

export class Enrollment{
    student:Student;
    class:Class;
    group:Group;
    finalGrade:number;
}