export class  WeekDayAndWeekNumber{
  public DaysInWeek:Array<string>;
  public WeekNumber:number;

  public constructor(DaysInWeek:Array<string>,WeekNumber:number){
    this.DaysInWeek=DaysInWeek;
    this.WeekNumber=WeekNumber;
  }

  public ReturnWeekNumber(){
    return this.WeekNumber;
  }

  public ReturnDaysInWeek(){
    return this.DaysInWeek;
  }
}
