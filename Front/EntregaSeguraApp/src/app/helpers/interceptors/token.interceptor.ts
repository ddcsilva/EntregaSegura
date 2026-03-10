import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { EMPTY, catchError, throwError } from 'rxjs';
import { AutenticacaoService } from '@app/services/autenticacao.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const autenticacaoService = inject(AutenticacaoService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  const tokenUsuario = autenticacaoService.obterToken();

  if (tokenUsuario) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${tokenUsuario}`,
      },
    });
  }

  return next(req).pipe(
    catchError((erro: HttpErrorResponse) => {
      if (erro.status === 401) {
        if (autenticacaoService.tokenExpirado()) {
          toastr.error(
            'Sua sessão expirou, por favor, faça login novamente',
            'Atenção!'
          );
        } else {
          toastr.error('Você não está autenticado!', 'Atenção!');
        }
        router.navigate(['login']);
        return EMPTY;
      } else {
        return throwError(() => erro);
      }
    })
  );
};
