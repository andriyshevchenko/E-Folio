import { Component, OnInit } from '@angular/core';
import { AdministrationService } from 'src/app/services/administration.service';
import { User } from 'src/app/components/models/user.model';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-administration-list',
  templateUrl: './administration-list.component.html',
  styleUrls: ['./administration-list.component.scss']
})
export class AdministrationListComponent implements OnInit {
  public displayedColumns: string[] = ['id', 'firstName', 'lastName', 'userName', 'email', 'emailConfirmed'];
  public dataSource = new MatTableDataSource();
  public users: User[] = [];

  constructor(private administrationService: AdministrationService) { }

  ngOnInit() {
    this.users = [];
    this.administrationService.getAllUsers()
      .subscribe(responce => {
        this.getData(responce);
        this.dataSource.data = this.users;
      },
        (error) => console.log(error)
      );
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
