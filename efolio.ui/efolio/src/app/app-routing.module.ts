import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProjectsComponent } from './components/projects/projects.component';
import { DevelopersComponent } from './components/developers/developers.component';
import { AdministrationComponent } from './components/administration/administration.component';
import { SupportComponent } from './components/support/support.component';
import { AccountComponent } from './components/account/account.component';
import { ProjectPageComponent } from './components/projects/project-page/project-page.component';
import { ProjectListComponent } from './components/projects/project-list/project-list.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [{
  path: '',
  redirectTo: 'projects',
  pathMatch: 'full'
}, {
  path: 'projects',
  component: ProjectsComponent,
  children: [
    { path: '', component: ProjectListComponent },
    { path: ':id', component: ProjectPageComponent }
  ]
}, {
  path: 'developers',
  component: DevelopersComponent
}, {
  path: 'support',
  component: SupportComponent
}, {
  path: 'administration',
  component: AdministrationComponent,
  canActivate: [AuthGuard]
}, {
  path: 'account',
  component: AccountComponent
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
