export class School{
    public ID:string;
    public Name:string;
    public StreetName:string;
    public HouseNumber:number;
    public PostalCode:string;
    public City:string;
    public Country:string;

    public constructor(id:string,name:string,street:string,houseNumber:number,postalCode:string,city:string,country:string){
        this.ID=id;
        this.Name=name;
        this.StreetName=street;
        this.HouseNumber=houseNumber;
        this.PostalCode=postalCode;
        this.City=city;
        this.Country=country;
    }
}