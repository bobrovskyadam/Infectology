import { Component, signal, inject } from '@angular/core';
import { ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';

import { AuthService } from '../../../services/auth.service';
import { LoginRequest } from '../../../models/login-request.model';
import { Router } from '@angular/router';

@Component({
  imports: [MatFormFieldModule, MatIconModule, MatInputModule, MatCardModule, MatButtonModule, ReactiveFormsModule],
  selector: 'app-login.component',
  styleUrl: './login.component.scss',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  hide = signal(true);
  loading = signal(false);
  errorMessage = signal('');
  successMessage = signal('')

  loginForm = new FormGroup({
    email: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.email],
    }),
    password: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required],
    }),
  });

  showPassword() {
    this.hide.update(current => !current);
  }

  onSubmit() {
    if (this.loginForm.invalid || this.loading()) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.errorMessage.set('');
    this.successMessage.set('');
    this.loading.set(true);

    const credentials: LoginRequest = this.loginForm.getRawValue();

    this.authService.login(credentials).subscribe({
      next: (response) => {
        console.log('Prihlasenie uspesne, rola: ', response.role);
        this.router.navigate(['/home']);
      },

      error: (error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.errorMessage.set('Nesprávny e-mail alebo heslo.')
        } else if (error.status === 0) {
          this.errorMessage.set(
            'Nepodarilo sa spojit so serverom'
          );
        } else {
          this.errorMessage.set(
            `Prihlasenie zlyhalo. Http chyba: ${error.status}`
          );
        }

        this.loading.set(false);
      },
      complete: () => {
        this.loading.set(false);
      }
    });
  }
}
