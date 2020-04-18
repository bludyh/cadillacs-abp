import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MainPageComponent } from './main-page/main-page.component';
import { SideBarComponent } from './main-page/side-bar/side-bar.component';
import { MainContentComponent } from './main-page/main-content/main-content.component';
import { NotificationBarComponent } from './main-page/notification-bar/notification-bar.component';
import { NavigationBarComponent } from './main-page/navigation-bar/navigation-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    SideBarComponent,
    MainContentComponent,
    NotificationBarComponent,
    NavigationBarComponent,
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
