import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs';

import { AutenticacaoService } from '@core/services';
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
  public readonly autenticacaoService = inject(AutenticacaoService);

  public readonly mostrarSenha = signal(false);

  public readonly formularioLogin = this.fb.group({
    login: ['', [Validators.required, Validators.email]],
    senha: ['', [Validators.required, Validators.minLength(6)]],
  });

  aoEntrar(): void {
    if (this.formularioLogin.invalid) return;

    const credenciais: LoginRequest = this.formularioLogin.value as LoginRequest;

    this.autenticacaoService
      .login(credenciais)
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
          // Erro será tratado pelo AutenticacaoService
          // Reset apenas a senha por segurança
          this.formularioLogin.patchValue({ senha: '' });
        },
      });
  }

  alternarVisibilidadeSenha(): void {
    this.mostrarSenha.update(mostrar => !mostrar);
  }

  verificarCampoInvalido(nomeCampo: string): boolean {
    const campo = this.formularioLogin.get(nomeCampo);
    return !!(campo && campo.invalid && (campo.dirty || campo.touched));
  }
}
