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
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
