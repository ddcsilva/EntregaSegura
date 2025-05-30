import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs';

import { AuthService } from '@core/services';
import { ButtonComponent } from '@ui/components/button/button.component';
import { LoginRequest } from '@core/models';

@Component({
  selector: 'es-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonComponent],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  public readonly authService = inject(AuthService);

  // Signals para estado local
  public readonly showPassword = signal(false);

  // Formulário reativo
  public readonly loginForm = this.fb.group({
    login: ['', [Validators.required, Validators.email]],
    senha: ['', [Validators.required, Validators.minLength(6)]],
  });

  // Submeter formulário
  onSubmit(): void {
    if (this.loginForm.invalid) return;

    const credentials: LoginRequest = this.loginForm.value as LoginRequest;

    this.authService
      .login(credentials)
      .pipe(
        finalize(() => {
          // Form será resetado automaticamente em caso de erro
          // O loading será gerenciado pelo AuthService
        })
      )
      .subscribe({
        next: () => {
          // Sucesso - redirecionar
          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/dashboard';
          this.router.navigate([returnUrl]);
        },
        error: () => {
          // Erro será tratado pelo AuthService
          // Reset apenas a senha por segurança
          this.loginForm.patchValue({ senha: '' });
        },
      });
  }

  // Toggle visibilidade da senha
  togglePasswordVisibility(): void {
    this.showPassword.update(show => !show);
  }

  // Verificar se campo tem erro
  isFieldInvalid(fieldName: string): boolean {
    const field = this.loginForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }
}
