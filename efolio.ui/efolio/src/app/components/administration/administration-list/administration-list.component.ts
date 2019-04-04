import { Component } from '@angular/core';
import { AdministrationService } from 'src/app/services/administration.service';
import { User } from 'src/app/components/models/user.model';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-administration-list',
  templateUrl: './administration-list.component.html',
  styleUrls: ['./administration-list.component.scss']
})
export class AdministrationListComponent {
  public showUsersTable: boolean = false;
  public showProjectsTable: boolean = false;
  public showDevelopersTable: boolean = false;
  public users: User[] = [];
  public displayedColumns: string[] = ['id', 'firstName', 'lastName', 'userName', 'email', 'emailConfirmed'];
  public dataSource = new MatTableDataSource();

  constructor(private administrationService: AdministrationService) { }

  showAllUsers() {
    this.showUsersTable = true;
    this.showProjectsTable = false;
    this.showDevelopersTable = false;
    this.users = [];
    this.administrationService.getAllUsers()
      .subscribe(responce => {
        this.getData(responce);
        this.dataSource.data = this.users;
      },
        (error) => console.log(error)
      );
  }

  showAllProjects() {
    this.showUsersTable = false;
    this.showProjectsTable = true;
    this.showDevelopersTable = false;
  }

  showAllDevelopers() {
    this.showUsersTable = false;
    this.showProjectsTable = false;
    this.showDevelopersTable = true;
  }

  getData(responce) {
    responce.forEach(element => {
      this.users.push(new User(element.id,
        element.firstName,
        element.lastName,
        element.userName,
        element.email,
        element.emailConfirmed));
    });
  }
}
