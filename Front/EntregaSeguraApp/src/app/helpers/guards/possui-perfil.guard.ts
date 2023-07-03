import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { UsuarioService } from '@app/services/usuario.service';
import { ToastrService } from 'ngx-toastr';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PossuiPerfilGuard implements CanActivate {
  constructor(
    private usuarioService: UsuarioService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.usuarioService.obterPerfilDaClaim().pipe(
      map(perfilUsuario => {
        if (route.data['perfis'].includes(perfilUsuario)) {
          return true;
        } else {
          this.router.navigate(['/']);
          this.toastr.warning('Você não tem permissão para acessar esta página!');
          return false;
        }
      })
    );
  }
}
