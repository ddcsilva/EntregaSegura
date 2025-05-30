import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

import { AutenticacaoService } from '@core/services/autenticacao.service';

/**
 * Guarda de autenticação
 * @param route - Rota ativada
 * @param state - Estado da rota
 * @returns true se o usuário está autenticado, false caso contrário
 */
export const autenticacaoGuard: CanActivateFn = (route, state) => {
  const autenticacaoService = inject(AutenticacaoService);
  const router = inject(Router);

  if (autenticacaoService.estaAutenticado()) {
    return true;
  }

  router.navigate(['/autenticacao/login'], {
    queryParams: { returnUrl: state.url },
  });

  return false;
};
