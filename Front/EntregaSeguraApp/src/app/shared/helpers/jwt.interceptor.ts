import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AutenticacaoService } from 'src/app/services/autenticacao.service';
import { environment } from 'src/environments/environment';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private autenticacaoService: AutenticacaoService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Adiciona o cabeçalho de autenticação com o token JWT se o usuário estiver logado e a solicitação for para a URL da API
        const usuario = this.autenticacaoService.usuarioAutenticado;
        const estaLogado = usuario?.token;
        const ehApiUrl = request.url.startsWith(environment.urlBaseApi);

        if (estaLogado && ehApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${usuario.token}`
                }
            });
        }

        return next.handle(request);
    }
}
