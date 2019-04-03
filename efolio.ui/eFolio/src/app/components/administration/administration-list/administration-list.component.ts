import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AdministrationService } from 'src/app/services/administration.service';
import { User } from 'src/app/components/models/user.model';
import { MatTableDataSource } from '@angular/material';
@Component({
  selector: 'app-administration-list',
  templateUrl: './administration-list.component.html',
  styleUrls: ['./administration-list.component.scss']
})
export class AdministrationListComponent implements OnInit {
  // @Output() public showSpinner = new EventEmitter<any>();

  users: User[] = [];

  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'userName', 'email', 'emailConfirmed'];
  dataSource = new MatTableDataSource();
  constructor(private administrationService: AdministrationService) { }

  ngOnInit() {
  }

  showAllUsers() {
    this.administrationService.getAllUsers()
      .subscribe(responce => {
        this.getData(responce);
        this.dataSource.data = this.users;
        console.log(responce);
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
