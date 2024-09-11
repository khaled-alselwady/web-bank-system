import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainStartupComponent } from './components/main-startup/main-startup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ClientsComponent } from './components/clients/clients.component';

const appRoutes: Routes = [
  { path: '', component: MainStartupComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'clients', component: ClientsComponent },
  { path: '**', component: MainStartupComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
