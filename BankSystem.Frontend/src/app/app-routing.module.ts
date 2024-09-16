import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainStartupComponent } from './components/main-startup/main-startup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ClientsComponent } from './components/clients/clients.component';
import { UsersComponent } from './components/users/users.component';
import { AddEditClientComponent } from './components/clients/add-edit-client/add-edit-client.component';
import { AddEditUserComponent } from './components/users/add-edit-user/add-edit-user.component';

const appRoutes: Routes = [
  { path: '', component: MainStartupComponent },
  { path: 'dashboard', component: DashboardComponent },
  {
    path: 'clients',
    component: ClientsComponent,
    children: [
      {
        path: 'new',
        component: AddEditClientComponent,
      },
      {
        path: ':clientId',
        component: AddEditClientComponent,
      },
    ],
  },
  {
    path: 'users',
    component: UsersComponent,
    children: [
      {
        path: 'new',
        component: AddEditUserComponent,
      },
      {
        path: ':userId',
        component: AddEditUserComponent,
      },
    ],
  },
  { path: '**', component: MainStartupComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
