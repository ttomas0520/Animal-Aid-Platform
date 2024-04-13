import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { RouterModule } from '@angular/router';
import { ImportModule } from '../../../modules/common/import.module';
@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [ImportModule],
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.css',
})
export class UserLoginComponent {
  hide = true;
  loginForm!: FormGroup;
  formBuilder = inject(FormBuilder);
  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  async loginSubmit() {
    var res = await this.authService.login(this.loginForm.value);
    console.log(this.loginForm.value);
  }
}
