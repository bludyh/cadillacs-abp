export class CalendarSubjectObject {
    public Subject: string;
    public Date: string;
    public Time: string;
    public TeacherName: string;
  
    constructor(subject: string, date: string, time: string, teacherName: string) {
      this.Subject = subject;
      this.Date = date;
      this.Time = time;
      this.TeacherName = teacherName;
    }
  
  
  }
  