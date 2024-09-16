import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';

import { LoginScreenService } from './services/login-screen.service';
import { ClientsService } from './services/clients.service';
import { ClientsDataService } from './services/clients-data.service';
import { UsersService } from './services/users.service';
import { UsersDataService } from './services/users-data.service';
import { FormService } from './services/form.service';

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
import { PaginatorComponent } from './components/shared/data-grid/paginator/paginator.component';
import { AddEditPersonComponent } from './components/people/add-edit-person/add-edit-person.component';
import { ErrorMessageInputComponent } from './components/shared/error-message-input/error-message-input.component';
import { AddEditClientComponent } from './components/clients/add-edit-client/add-edit-client.component';
import { StatueColorPipe } from './pipes/status-color.pipe';
import { AlertComponent } from './components/shared/alert/alert.component';
import { HeaderItemService } from './services/header-item.service';

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
    AddEditPersonComponent,
    ErrorMessageInputComponent,
    AddEditClientComponent,
    AlertComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    NoopAnimationsModule,
    MatTableModule,
    ReactiveFormsModule,
  ],
  providers: [
    LoginScreenService,
    ClientsService,
    ClientsDataService,
    UsersService,
    UsersDataService,
    FormService,
    HeaderItemService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
