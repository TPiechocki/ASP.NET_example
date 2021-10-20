import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SatellitesComponent } from './satellites/satellites.component';
import { JwtModule } from '@auth0/angular-jwt';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FormsModule } from '@angular/forms';
import { SatellitesObsoleteComponent } from './satellites-obsolete/satellites-obsolete.component';

@NgModule({
  declarations: [
    AppComponent,
    SatellitesComponent,
    LoginComponent,
    DashboardComponent,
    SatellitesObsoleteComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('access_token');
        },
        allowedDomains: ['localhost:5001'],
        disallowedRoutes: ['localhost:5001/api/v1/auth/login', 'localhost:5001/api/v2/auth/login']
      }
    }),
    FormsModule
  ],
  providers: [
    LoginComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
