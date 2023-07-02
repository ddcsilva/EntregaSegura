import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AutenticacaoService } from '@app/services/autenticacao.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AutenticacaoGuard implements CanActivate {

  constructor(
    private autenticacaoService: AutenticacaoService, 
    private router: Router,
    private toastr: ToastrService) { }

  canActivate(): boolean {
    if (this.autenticacaoService.usuarioEstaAutenticado()) {
      return true;
    } else {
      this.toastr.error('Você não está autenticado!', 'Atenção!');
      this.router.navigate(['login']);
      return false;
    }
  }
}
 