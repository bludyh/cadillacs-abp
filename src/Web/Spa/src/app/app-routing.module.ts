import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';
import { CoursesComponent } from './main-page/main-content/courses/courses.component';
import { MainContentComponent } from './main-page/main-content/main-content.component';
import { DashboardComponent } from './main-page/main-content/dashboard/dashboard.component';
import { SubmissionsComponent } from './main-page/main-content/submissions/submissions.component';
import { ProgressComponent } from './main-page/main-content/progress/progress.component';
import { CalendarComponent } from './main-page/main-content/calendar/calendar.component';
import { StudentProfileComponent } from './main-page/main-content/student-profile/student-profile.component';

const routes: Routes = [
    {path: 'student' , component: DashboardComponent},
    {path: 'student/Dashboard' , component: DashboardComponent},
    {path: 'student/Courses' , component: CoursesComponent},
    {path: 'student/Submissions' , component: SubmissionsComponent},
    {path: 'student/Progress' , component: ProgressComponent},
    {path: 'student/Calendar' , component: CalendarComponent},
    {path: 'student/Profile' , component: StudentProfileComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }