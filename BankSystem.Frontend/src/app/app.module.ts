import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';

import { LoginScreenService } from './services/login-screen.service';
import { UsersService } from './services/users.service';
import { ClientsService } from './services/clients.service';

import { AppComponent } from './app.component';
import { ItemSidebarComponent } from './components/item-sidebar/item-sidebar.component';
import { HeaderComponent } from './components/header/header.component';
import { MainItemsSidebarComponent } from './components/main-items-sidebar/main-items-sidebar.component';
import { AuthComponent } from './components/auth/auth.component';
import { ErrorMessageComponent } from './components/shared/error-message/error-message.component';
import { MainStartupComponent } from './components/main-startup/main-startup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardItemComponent } from './components/dashboard/dashboard-item/dashboard-item.component';
import { ClientsComponent } from './components/clients/clients.component';
import { HeaderItemComponent } from './components/shared/header-item/header-item.component';
import { DataGridComponent } from './components/shared/data-grid/data-grid.component';
import { UsersComponent } from './components/users/users.component';
import { StatueColorPipe } from './pipes/status-color.pipe';
import { PaginatorComponent } from './components/shared/data-grid/paginator/paginator.component';

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
    ClientsComponent,
    HeaderItemComponent,
    DataGridComponent,
    StatueColorPipe,
    UsersComponent,
    PaginatorComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    NoopAnimationsModule,
    MatTableModule,
  ],
  providers: [
    LoginScreenService,
    UsersService,
    ClientsService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
