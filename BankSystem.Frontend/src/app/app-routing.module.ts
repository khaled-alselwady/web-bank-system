import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainStartupComponent } from './main-startup/main-startup.component';

const appRoutes: Routes = [
  { path: '', component: MainStartupComponent },
  { path: '**', component: MainStartupComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
