import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { EMPTY, Observable, catchError, throwError } from 'rxjs';
import { AutenticacaoService } from '@app/services/autenticacao.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(
    private autenticacaoService: AutenticacaoService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const tokenUsuario = this.autenticacaoService.obterToken();

    if (tokenUsuario) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${tokenUsuario}`
        }
      });
    }

    return next.handle(request).pipe(
      catchError((erro: HttpErrorResponse) => {
        if (erro.status === 401) {
          if (this.autenticacaoService.tokenExpirado()) {
            this.toastr.error('Sua sessão expirou, por favor, faça login novamente', 'Atenção!');
          } else {
            this.toastr.error('Você não está autenticado!', 'Atenção!');
          }
          this.router.navigate(['login']);
          return EMPTY;
        } else {
          return throwError(() => erro);
        }
      })
    );
  }
}