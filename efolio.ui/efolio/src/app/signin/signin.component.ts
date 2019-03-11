import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})

export class SigninComponent implements OnInit {
 
  public readonly passwordsAreNotEqualMessage = "The passwords are not equal";  

  public signinForm: FormGroup; 

  onSubmit() {  
    console.log(JSON.stringify(this.signinForm.value));
  }

  constructor() { }

  ngOnInit() {
    this.signinForm = new FormGroup({ 
      "email": new FormControl(null, [Validators.required, Validators.email]), 
      "password": new FormControl(
        null, [Validators.required, Validators.minLength(9)]
       )
    });
  }
}
