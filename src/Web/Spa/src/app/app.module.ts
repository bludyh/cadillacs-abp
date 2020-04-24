import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MainPageComponent } from './main-page/main-page.component';
import { SideBarComponent } from './main-page/side-bar/side-bar.component';
import { MainContentComponent } from './main-page/main-content/main-content.component';
import { NotificationBarComponent } from './main-page/notification-bar/notification-bar.component';
import { NavigationBarComponent } from './main-page/navigation-bar/navigation-bar.component';
import { AppRoutingModule } from './app-routing.module';
import { SettingsComponent } from './main-page/settingsPage/settings.component';
import { ProfileComponent } from './main-page/profile/profile.component';
import { LogoutComponent } from './main-page/logout/logout.component';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    SideBarComponent,
    MainContentComponent,
    NotificationBarComponent,
    NavigationBarComponent,
    SettingsComponent,
    ProfileComponent,
    LogoutComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
