import { Component } from '@angular/core';
import { UserLoggingService } from 'src/app/services/user-logging.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidationService } from 'src/app/services/validation.service';
import {
  MatSnackBar,
  MatSnackBarConfig,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material';
import { Router } from '@angular/router';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {
  public hidePassword = true;
  public hideConfirmPassword = true;
  public registerForm: any;

  constructor(private userLoggingService: UserLoggingService,
    public validationService: ValidationService,
    private router: Router,
    public loginValidatorBar: MatSnackBar,
    private loaderService: LoaderService) {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required])
    });
  }

  onSignUp() {
    this.loaderService.startLoading();
    if (this.registerForm.valid) {
      const formData = {
        firstName: this.registerForm.value.firstName,
        lastName: this.registerForm.value.lastName,
        email: this.registerForm.value.email,
        password: this.registerForm.value.password
      };
      this.userLoggingService.signUp(formData)
        .subscribe(
        );
      this.loginValidatorBar.open("You are registered in eFolio", "OK", {
        duration: 5000,
        panelClass: ['snackBar'],
      });
      this.loaderService.stopLoading();
    }
    this.registerForm.reset();
  }
}
