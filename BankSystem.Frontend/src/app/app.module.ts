import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { LoginScreenService } from './services/login-screen.service';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ItemSidebarComponent } from './components/item-sidebar/item-sidebar.component';
import { UsersService } from './services/users.service';
import { HeaderComponent } from './components/header/header.component';
import { MainItemsSidebarComponent } from './components/main-items-sidebar/main-items-sidebar.component';
import { AuthComponent } from './components/auth/auth.component';
import { ErrorMessageComponent } from './components/shared/error-message/error-message.component';
import { MainStartupComponent } from './components/main-startup/main-startup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardItemComponent } from './components/dashboard/dashboard-item/dashboard-item.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainStartupComponent,
    AuthComponent,
    ErrorMessageComponent,
    MainItemsSidebarComponent,
    ItemSidebarComponent,
    DashboardComponent,
    DashboardItemComponent,
  ],
  imports: [BrowserModule, FormsModule, HttpClientModule, AppRoutingModule],
  providers: [LoginScreenService, UsersService],
  bootstrap: [AppComponent],
})
export class AppModule {}
