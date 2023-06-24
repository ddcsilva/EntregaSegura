import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';

import { AutenticacaoService } from '@app/services';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private autenticacaoService: AutenticacaoService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const usuario = this.autenticacaoService.usuarioAutenticado;

    if (usuario) {
      // Verifica se a rota é restrita pelo perfil
      const { roles } = route.data;

      if (roles && !roles.includes(usuario.roles)) {
        // Role não autorizada, então redireciona para a página inicial
        this.router.navigate(['/']);
        return false;
      }

      // Se o usuário estiver autenticado e a rota for permitida, então retorna true
      return true;
    }
    
    // Se o usuário não estiver autenticado, então redireciona para a página de login
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}