import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

import { AuthService } from '@core/services/auth.service';

/**
 * Guarda de autenticação
 * @param route - Rota atual
 * @param state - Estado da rota
 * @returns true se o usuário está autenticado, false caso contrário
 */
export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isAuthenticated()) {
    return true;
  }

  router.navigate(['/auth/login'], {
    queryParams: { returnUrl: state.url },
  });

  return false;
};
