import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';

import { AutenticacaoService } from '@core/services/autenticacao.service';
import { TokenStorageService } from '@core/services/token-storage.service';
import { LoggerService } from '@core/services/logger.service';
import { environment } from '@environments';

export const autenticacaoInterceptor: HttpInterceptorFn = (req, next) => {
  const autenticacaoService = inject(AutenticacaoService);
  const tokenStorage = inject(TokenStorageService);
  const logger = inject(LoggerService);

  const urlsPublicas = [`${environment.api.endpoints.autenticacao}/autenticacao`, '/api/public'];

  const ehUrlPublica = urlsPublicas.some(url => req.url.includes(url));

  if (ehUrlPublica) {
    logger.debug('Requisição para URL pública, sem token', { url: req.url }, 'AuthInterceptor');
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

      logger.debug(
        'Token adicionado à requisição',
        {
          url: req.url,
          method: req.method,
        },
        'AuthInterceptor'
      );

      return next(authReq).pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            logger.warn(
              'Token rejeitado pelo servidor (401)',
              {
                url: error.url,
                status: error.status,
              },
              'AuthInterceptor'
            );

            autenticacaoService.logout();
          }
          return throwError(() => error);
        })
      );
    } else {
      logger.debug(
        'Token não disponível ou expirado',
        {
          hasToken: !!token,
          url: req.url,
        },
        'AuthInterceptor'
      );
    }
  } catch (error) {
    // Se houver erro na obtenção ou verificação do token, continua sem token
    logger.warn('Erro ao processar token', error, 'AuthInterceptor');
  }

  return next(req);
};
