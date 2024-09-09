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

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainStartupComponent,
    AuthComponent,
    ErrorMessageComponent,
  ],
  imports: [BrowserModule, FormsModule, HttpClientModule],
  providers: [LoginScreenService],
  bootstrap: [AppComponent],
})
export class AppModule {}
