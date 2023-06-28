import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';

@Injectable({
  providedIn: 'root'
})
export class ErroInterceptor implements HttpInterceptor {

  constructor(private autenticacaoService: AutenticacaoService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(e => {
      if ([401, 403].includes(e.status) && this.autenticacaoService.usuarioAutenticado) {
        // Realiza o logout automático se a API retornar os status 401 ou 403
        this.autenticacaoService.logout();
      }
      
      const erro = e.error.message || e.statusText;
      return throwError(() => erro);
    }));
  }
}
