import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';

import { LoginComponent } from './login/login.component';
import { SatellitesObsoleteComponent } from './satellites-obsolete/satellites-obsolete.component';
import { SatellitesComponent } from './satellites/satellites.component';

const routes: Routes = [
  {
     path: 'satellites',
     canActivate: [AuthGuard],
     component: SatellitesComponent
  },
  {
    path: 'satellites-obsolete',
    canActivate: [AuthGuard],
    component: SatellitesObsoleteComponent
 },
  {
    path: 'login',
    component: LoginComponent,
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
