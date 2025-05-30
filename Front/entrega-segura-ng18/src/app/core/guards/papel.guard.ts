import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

import { AuthService } from '@core/services/auth.service';

/**
 * Guarda de rota baseada em papéis
 * @param papeisPermitidos - Lista de papéis permitidos
 * @returns true se o usuário tem um papel permitido, false caso contrário
 */
export const papelGuard = (papeisPermitidos: string[]): CanActivateFn => {
  return () => {
    const authService = inject(AuthService);
    const router = inject(Router);

    if (!authService.isAuthenticated()) {
      router.navigate(['/auth/login']);
      return false;
    }

    const papelUsuario = authService.userRole();

    if (papelUsuario && papeisPermitidos.includes(papelUsuario)) {
      return true;
    }

    router.navigate(['/dashboard']);
    return false;
  };
};
