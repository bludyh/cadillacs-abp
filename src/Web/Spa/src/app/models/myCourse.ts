import { Assignment } from './assignment';

export class myCourse{
   
    public Name:string;
    public Description:string;
    public Type:string;

    date: Date = new Date();  
    public assignments: Assignment[] = [];

    public constructor(name:string,type:string,description:string){
        this.Type = type;
        this.Description= description;
        this.Name = name;      
    }

    
}