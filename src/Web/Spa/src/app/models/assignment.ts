export class Assignment{
    public DueTime?:Date;
    public Name?:string;
    public CourseName?:string;
    public Description?:string;

    public constructor(name:string, dueTime:Date,courseName:string,description:string){
        this.DueTime=dueTime;
        this.Name=name;
        this.CourseName=courseName;
        this.Description=description;
    }
}