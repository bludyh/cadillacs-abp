import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavigationBarComponent } from './main-page/navigation-bar/navigation-bar.component';
import { SettingsComponent } from './main-page/settingsPage/settings.component';
import { LogoutComponent } from './main-page/logout/logout.component';
import { ProfileComponent } from './main-page/profile/profile.component';

const routes : Routes = [
  {path: 'navigationBar', component:NavigationBarComponent },
  {path: 'logout', component:LogoutComponent },
  {path: 'profile', component:ProfileComponent },
  {path: 'settings', component:SettingsComponent }];

@NgModule({
  imports:[RouterModule.forRoot(routes)],
  exports:[RouterModule],
})
export class AppRoutingModule { }
