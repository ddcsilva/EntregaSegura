import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';

import { AutenticacaoService } from '@core/services/autenticacao.service';
import { TokenStorageService } from '@core/services/token-storage.service';
import { environment } from '@environments';

export const autenticacaoInterceptor: HttpInterceptorFn = (req, next) => {
  const autenticacaoService = inject(AutenticacaoService);
  const tokenStorage = inject(TokenStorageService);

  const urlsPublicas = [`${environment.api.endpoints.autenticacao}/autenticacao`, '/api/public'];

  const ehUrlPublica = urlsPublicas.some(url => req.url.includes(url));

  if (ehUrlPublica) {
    return next(req);
  }

  try {
    const token = tokenStorage.obterToken();

    if (token && !tokenStorage.verificarTokenExpirado(token)) {
      const authReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });

      return next(authReq).pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            autenticacaoService.logout();
          }
          return throwError(() => error);
        })
      );
    }
  } catch (error) {
    // Se houver erro na obtenção ou verificação do token, continua sem token
    console.warn('Erro ao processar token:', error);
  }

  return next(req);
};
