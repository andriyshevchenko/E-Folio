import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainNavComponent } from './components/main-nav/main-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { 
  MatToolbarModule, 
  MatButtonModule, 
  MatSidenavModule, 
  MatIconModule, 
  MatListModule, 
  MatInputModule, 
  MatFormFieldModule, 
  MatGridListModule
} from '@angular/material';
import {MatCardModule} from '@angular/material/card';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProjectsComponent } from './components/projects/projects.component';
import { DevelopersComponent } from './components/developers/developers.component';
import { SupportComponent } from './components/support/support.component';
import { AdministrationComponent } from './components/administration/administration.component';
import { AccountComponent } from './components/account/account.component';
import { ProjectFilterComponent } from './components/projects/project-filter/project-filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { UserLoggingComponent } from './components/account/user-logging/user-logging.component'
//import { UserLoggingService } from './services/user-logging.service';
// import { UserSignInInfo } from './models/user-signin-info.model';
import { HttpClientModule } from '@angular/common/http';
//import { SignUpComponent } from './components/account/user-logging/sign-up/sign-up.component';
//import { SignInComponent } from './components/account/user-logging/sign-in/sign-in.component';
import { ProjectListComponent } from './components/projects/project-list/project-list.component'
import { ProjectItemComponent } from './components/projects/project-list/project-item/project-item.component';
import { ProjectService } from './services/project.service';
import { CardDirective } from './components/directives/card.directive';

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
    // UserLoggingComponent,
    // SignUpComponent,
    // SignInComponent,
    ProjectListComponent,
    ProjectItemComponent,
    CardDirective
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
    HttpClientModule,
    ReactiveFormsModule,
    MatCardModule,
    MatGridListModule
  ],
  //providers: [UserLoggingService, ProjectService],
  providers: [ProjectService],
  bootstrap: [AppComponent]
})
export class AppModule { }
