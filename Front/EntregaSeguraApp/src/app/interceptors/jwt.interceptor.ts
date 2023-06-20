import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { User } from '../models/user';
import { ContaService } from '../services/usuario/conta.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private contaService: ContaService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let usuarioAtual: User | null = null;

    this.contaService.currentUser$.pipe(take(1)).subscribe(user => {
      usuarioAtual = user;

      if (usuarioAtual) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${usuarioAtual.token}`
          }
        });
      }
    });

    return next.handle(request);
  }
}
