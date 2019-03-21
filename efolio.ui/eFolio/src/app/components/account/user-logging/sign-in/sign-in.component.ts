import { Component, OnInit } from '@angular/core';

import { UserLoggingService } from 'src/app/services/user-logging.service';
import { FormControl, Validators, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {
  hide = true;
  public loginForm: any;

  constructor(private userLoggingService: UserLoggingService) {
    this.loginForm = new FormGroup({
      "email": new FormControl('', [Validators.required, Validators.email,]),
      "password": new FormControl('', [Validators.required])
    });
  }

  onSignIn() {
    if (this.loginForm.valid) {
      const formData = {
        email: this.loginForm.value.email,
        password: this.loginForm.value.password
      };
      this.userLoggingService.signIn(formData)
        .subscribe(
          response => { localStorage.setItem('accessToken', response['accessToken']); console.log(response);},
          error => console.log(error)
        );
    }
  }

  test(){
    //var currentUser = JSON.parse(localStorage.getItem('access_token'));
    //var token = currentUser.token;
    console.log(localStorage.getItem('accessToken'));
  }
  ngOnInit() {
  }

}
