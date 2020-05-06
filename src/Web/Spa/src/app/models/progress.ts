export class Progress{
    public CurrentECs:number;
    public TotalECs:number;
    public Percentage:number;
    public Description:string;

    public constructor(currentECs:number,totalECs:number,description:string){
        this.CurrentECs=currentECs;
        this.TotalECs=totalECs;
        this.Percentage=(this.CurrentECs/this.TotalECs)*100;
        this.Description=description;
    }
}