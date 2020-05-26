import * as moment from 'moment';

export class CalendarWeek {
   private firstDayOfWeek;
   private daysOfWeek = [];

   public constructor(firstDayOfWeek: moment.Moment) {
     this.firstDayOfWeek = firstDayOfWeek;
     this.GenerateDateOfWeek();
   }

   public GenerateDateOfWeek() {
     for (let i = 0; i < 7; i++) {
       if (i === 0) {
         this.daysOfWeek.push(this.firstDayOfWeek.format('MMM DD').toString());
       } else {
         this.daysOfWeek.push(this.firstDayOfWeek.add(1, 'days').format('MMM DD').toString());
       }
     }
   }

   public ReturnDaysOfWeek() {
     return this.daysOfWeek;
   }
}
