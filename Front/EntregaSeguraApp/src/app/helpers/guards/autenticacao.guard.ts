import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AutenticacaoService } from '@app/services/autenticacao.service';

@Injectable({
  providedIn: 'root'
})
export class AutenticacaoGuard implements CanActivate {

  constructor(
    private autenticacaoService: AutenticacaoService, 
    private router: Router) { }

  canActivate(): boolean {
    if (this.autenticacaoService.usuarioEstaAutenticado()) {
      return true;
    } else {
      this.router.navigate(['login']);
      return false;
    }
  }
}