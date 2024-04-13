import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { _MatInternalFormField, MatOption } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatIcon } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { RouterModule } from '@angular/router';
import { ImportModule } from '../../../modules/common/import.module';

@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.css',
})
export class UserRegisterComponent {
  accountTypes: string[] = ['Civil', 'Állatvédő', 'Menhely'];
  selectedType = 'Civil';
  hide = true;
  registerForm!: FormGroup;
  formBuilder = inject(FormBuilder);

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
      type: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
    });
    this.registerForm.setValidators(confirmPasswordValidator);
  }
  get password() {
    return this.registerForm.get('password');
  }
  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }

  onPasswordInput() {
    if (this.registerForm.hasError('PasswordNoMatch'))
      this.confirmPassword!.setErrors([{ PasswordNoMatch: true }]);
    else this.confirmPassword!.setErrors(null);
  }

  registerSubmit() {
    console.log(this.registerForm.value);
    this.authService.register(this.registerForm.value);
  }
}

const confirmPasswordValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  return control.value.password === control.value.confirmPassword
    ? null
    : { PasswordNoMatch: true };
};
