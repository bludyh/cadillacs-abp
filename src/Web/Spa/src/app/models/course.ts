export class Course{
    public ImgSrc:string="https://i.imgur.com/AyVaERv.png";
    public CourseID:string;
    public CourseDescription:string;

    public constructor(id:string,description:string){
        this.CourseID=id;
        this.CourseDescription=description;
    }
}