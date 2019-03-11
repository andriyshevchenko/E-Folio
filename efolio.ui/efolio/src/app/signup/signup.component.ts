import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
 
  public readonly passwordsAreNotEqualMessage = "The passwords are not equal";  

  public signupForm: FormGroup; 

  onSubmit() { 
    const confirmedPassword = this.signupForm.get('confirmed_password');
    if(this.signupForm.get('password').value !== confirmedPassword.value){ 
      confirmedPassword.setErrors({"passwordsAreNotEqual": true});
    }
    console.log(JSON.stringify(this.signupForm.value));
  }

  constructor() { }

  ngOnInit() {
    this.signupForm = new FormGroup({
      "firstName": new FormControl(null, Validators.required),
      "lastName": new FormControl(null, Validators.required),
      "email": new FormControl(null, [Validators.required, Validators.email]),
      "password": new FormControl(null, Validators.required), 
      "confirmed_password": new FormControl(
        null, [Validators.required, Validators.minLength(9)]
       )
    });
  }
}
