import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { LoginScreenService } from './services/login-screen.service';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { MainStartupComponent } from './main-startup/main-startup.component';
import { AuthComponent } from './auth/auth.component';
import { ErrorMessageComponent } from './shared/error-message/error-message.component';
import { AppRoutingModule } from './app-routing.module';
import { MainItemsSidebarComponent } from './main-items-sidebar/main-items-sidebar.component';
import { ItemSidebarComponent } from './item-sidebar/item-sidebar.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainStartupComponent,
    AuthComponent,
    ErrorMessageComponent,
    MainItemsSidebarComponent,
    ItemSidebarComponent,
  ],
  imports: [BrowserModule, FormsModule, HttpClientModule, AppRoutingModule],
  providers: [LoginScreenService],
  bootstrap: [AppComponent],
})
export class AppModule {}
