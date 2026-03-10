import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

import { AutenticacaoService } from '@core/services/autenticacao.service';

/**
 * Guarda de rota baseada em papéis
 * @param papeisPermitidos - Lista de papéis permitidos
 * @returns true se o usuário tem um papel permitido, false caso contrário
 */
export const papelGuard = (papeisPermitidos: string[]): CanActivateFn => {
  return () => {
    const autenticacaoService = inject(AutenticacaoService);
    const router = inject(Router);

    if (!autenticacaoService.estaAutenticado()) {
      router.navigate(['/autenticacao/login']);
      return false;
    }

    const papelUsuario = autenticacaoService.papel();

    if (papelUsuario && papeisPermitidos.includes(papelUsuario)) {
      return true;
    }

    router.navigate(['/dashboard']);
    return false;
  };
};
