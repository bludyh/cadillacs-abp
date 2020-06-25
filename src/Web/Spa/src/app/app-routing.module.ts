import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';
import { CoursesComponent } from './main-page/main-content/courses/courses.component';
import { MainContentComponent } from './main-page/main-content/main-content.component';

const routes: Routes = [
    {path: 'student' , component: MainPageComponent},
    {path: 'student/Dashboard' , component: MainPageComponent},
    {path: 'student/Courses' , component: MainPageComponent},
    {path: 'student/Submissions' , component: MainPageComponent},
    {path: 'student/Progress' , component: MainPageComponent},
    {path: 'student/Calendar' , component: MainPageComponent},
    {path: 'student/Profile' , component: MainPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }