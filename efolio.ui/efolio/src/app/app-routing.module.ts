import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProjectsComponent} from './components/projects/projects.component';
import { DevelopersComponent} from './components/developers/developers.component';
import { AdministrationComponent} from './components/administration/administration.component';
import { SupportComponent} from './components/support/support.component';
import { AccountComponent} from './components/account/account.component';

const routes: Routes = [
  {path: 'projects', component: ProjectsComponent},
  {path: 'developers', component: DevelopersComponent},
  {path: 'support', component: SupportComponent},
  {path: 'administration', component: AdministrationComponent},
  {path: 'account', component: AccountComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
