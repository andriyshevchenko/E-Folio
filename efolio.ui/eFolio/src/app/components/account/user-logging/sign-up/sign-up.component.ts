import { Component } from '@angular/core';
import { UserLoggingService } from 'src/app/services/user-logging.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidationService } from 'src/app/services/validation.service';

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
              public validationService: ValidationService) {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required])
    });
  }

  onSignUp() {
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
    }
    this.registerForm.reset();
  }
}
