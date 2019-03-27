import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainNavComponent } from './components/main-nav/main-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import {
  MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule,
  MatInputModule, MatFormFieldModule
} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProjectsComponent } from './components/projects/projects.component';
import { DevelopersComponent } from './components/developers/developers.component';
import { SupportComponent } from './components/support/support.component';
import { AdministrationComponent } from './components/administration/administration.component';
import { AccountComponent } from './components/account/account.component';
import { ProjectFilterComponent } from './components/projects/project-filter/project-filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserLoggingComponent } from './components/account/user-logging/user-logging.component';
import { UserLoggingService } from './services/user-logging.service';
// import { UserSignInInfo } from './models/user-signin-info.model';
import { SignUpComponent } from './components/account/user-logging/sign-up/sign-up.component';
import { SignInComponent } from './components/account/user-logging/sign-in/sign-in.component';
import { ValidationService } from './services/validation.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    MainNavComponent,
    ProjectsComponent,
    DevelopersComponent,
    SupportComponent,
    AdministrationComponent,
    AccountComponent,
    ProjectFilterComponent,
    UserLoggingComponent,
    SignUpComponent,
    SignInComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    BrowserAnimationsModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [UserLoggingService, ValidationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
