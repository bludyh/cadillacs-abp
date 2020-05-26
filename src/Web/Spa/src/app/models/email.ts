export class Email{
    public Sender:string;
    public Time:string;
    public Subject:string;
    public Status:boolean;

    public constructor(sender:string,time:string,subject:string,status:boolean){
        this.Sender=sender;
        this.Time=time;
        this.Subject=subject;
        this.Status=status;
    }
}