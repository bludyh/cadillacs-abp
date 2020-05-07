import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MainPageComponent } from './main-page/main-page.component';
import { SideBarComponent } from './main-page/side-bar/side-bar.component';
import { MainContentComponent } from './main-page/main-content/main-content.component';
import { NotificationBarComponent } from './main-page/notification-bar/notification-bar.component';
import { NavigationBarComponent } from './main-page/navigation-bar/navigation-bar.component';
import { CoursesComponent } from './main-page/main-content/courses/courses.component';
import { DashboardComponent } from './main-page/main-content/dashboard/dashboard.component';
import { SubmissionsComponent } from './main-page/main-content/submissions/submissions.component';
import { ProgressComponent } from './main-page/main-content/progress/progress.component';
import { CalendarComponent } from './main-page/main-content/calendar/calendar.component';
import { FunctionItemComponent } from './main-page/side-bar/function-item/function-item.component';
import { CourseComponent } from './main-page/main-content/courses/course/course.component';
import { ProgressObjectComponent } from './main-page/main-content/progress/progress-object/progress-object.component';
import { CalendarDayComponent } from './main-page/main-content/calendar/calendar-day/calendar-day.component';
import { CalendarMonthComponent } from './main-page/main-content/calendar/calendar-month/calendar-month.component';
import { CalendarWeekComponent } from './main-page/main-content/calendar/calendar-week/calendar-week.component';
import { CalendarDayHourComponent } from './main-page/main-content/calendar/calendar-day/calendar-day-hour/calendar-day-hour.component';
import { CalendarWeekDayComponent } from './main-page/main-content/calendar/calendar-week/calendar-week-day/calendar-week-day.component';
import { CalendarWeekWeekComponent } from './main-page/main-content/calendar/calendar-week/calendar-week-week/calendar-week-week.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CalendarMonthDayComponent } from './main-page/main-content/calendar/calendar-month/calendar-month-day/calendar-month-day.component';
import {HttpClientModule} from '@angular/common/http';
import { ActivitiesComponent } from './main-page/main-content/dashboard/activities/activities.component';
import { ActivityComponent } from './main-page/main-content/dashboard/activities/activity/activity.component';
import { EmailsComponent } from './main-page/main-content/dashboard/emails/emails.component';
import { EmailComponent } from './main-page/main-content/dashboard/emails/email/email.component';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    SideBarComponent,
    MainContentComponent,
    NotificationBarComponent,
    NavigationBarComponent,
    CoursesComponent,
    DashboardComponent,
    SubmissionsComponent,
    ProgressComponent,
    CalendarComponent,
    FunctionItemComponent,
    CourseComponent,
    ProgressObjectComponent,
    CalendarDayComponent,
    CalendarMonthComponent,
    CalendarWeekComponent,
    CalendarDayHourComponent,
    CalendarWeekDayComponent,
    CalendarWeekWeekComponent,
    CalendarMonthDayComponent,
    ActivitiesComponent,
    ActivityComponent,
    EmailsComponent,
    EmailComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
