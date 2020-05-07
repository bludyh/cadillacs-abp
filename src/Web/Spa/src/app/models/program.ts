import { School } from './school';

export class Program{
    public ID:string;
    public Name:String;
    public School:School;

    public constructor(id:string,name:string,school:School){
        this.ID=id;
        this.Name=name;
        this.School=school;
    }
}