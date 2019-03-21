import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { UserLoggingService } from 'src/app/services/user-logging.service';


@Component({
  selector: 'app-user-logging',
  templateUrl: './user-logging.component.html',
  styleUrls: ['./user-logging.component.scss']
})
export class UserLoggingComponent implements OnInit {
  constructor(private userLoggingService: UserLoggingService) { }

  ngOnInit() {
  }

}
