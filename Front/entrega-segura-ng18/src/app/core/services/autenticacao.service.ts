import { Injectable, computed, signal, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, catchError, tap, throwError } from 'rxjs';

import { environment } from '@environments';
import { EstadoAutenticacao, LoginRequest, Autenticacao, Usuario, JwtPayload } from '@core/models';
import { Papel, ehPapelValido } from '@core/models';
import { TokenStorageService } from './token-storage.service';

@Injectable({
  providedIn: 'root',
})
export class AutenticacaoService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);
  private readonly tokenStorage = inject(TokenStorageService);

  private readonly urlApi = `${environment.api.baseUrl}${environment.api.endpoints.autenticacao}`;

  private readonly estadoAutenticacao = signal<EstadoAutenticacao>({
    usuario: null,
    token: null,
    carregando: false,
    erro: null,
  });

  public readonly usuario = computed(() => this.estadoAutenticacao().usuario);
  public readonly token = computed(() => this.estadoAutenticacao().token);
  public readonly carregando = computed(() => this.estadoAutenticacao().carregando);
  public readonly erro = computed(() => this.estadoAutenticacao().erro);

  public readonly estaAutenticado = computed(() => {
    const token = this.token();
    return !!token && !this.tokenStorage.verificarTokenExpirado(token);
  });

  public readonly papel = computed(() => this.usuario()?.papel ?? null);

  public readonly podeAcessarAdmin = computed(() => this.papel() === 'Administrador');

  public readonly podeAcessarSindico = computed(() => ['Administrador', 'Sindico'].includes(this.papel() ?? ''));

  constructor() {
    this.inicializarAutenticacao();
  }

  private inicializarAutenticacao(): void {
    const tokenArmazenado = this.tokenStorage.obterToken();

    if (tokenArmazenado && !this.tokenStorage.verificarTokenExpirado(tokenArmazenado)) {
      try {
        const payload = this.decodificarToken(tokenArmazenado);
        const usuario = this.payloadParaUsuario(payload);

        this.estadoAutenticacao.update(estado => ({
          ...estado,
          usuario,
          token: tokenArmazenado,
        }));
      } catch (error) {
        console.warn('Token inválido encontrado, removendo:', error);
        this.tokenStorage.removerToken();
      }
    }
  }

  login(credenciais: LoginRequest): Observable<Autenticacao> {
    this.definirCarregando(true);
    this.limparErro();

    return this.http.post<Autenticacao>(`${this.urlApi}/autenticacao`, credenciais).pipe(
      tap(resposta => this.processarLoginSucesso(resposta)),
      catchError(error => this.processarErroLogin(error, 'Erro ao fazer login'))
    );
  }

  logout(): void {
    this.tokenStorage.removerToken();

    this.estadoAutenticacao.set({
      usuario: null,
      token: null,
      carregando: false,
      erro: null,
    });

    this.router.navigate(['/autenticacao/login']);
  }

  private processarLoginSucesso(resposta: Autenticacao): void {
    const { token, usuario } = resposta;

    this.tokenStorage.salvarToken(token);

    this.estadoAutenticacao.update(estado => ({
      ...estado,
      usuario,
      token,
      carregando: false,
      erro: null,
    }));
  }

  private processarErroLogin(erro: unknown, mensagemPadrao: string): Observable<never> {
    let mensagemErro = mensagemPadrao;

    if (erro instanceof HttpErrorResponse) {
      if (erro.error?.message) {
        mensagemErro = erro.error.message;
      } else if (erro.status === 401) {
        mensagemErro = 'Credenciais inválidas';
      } else if (erro.status === 0) {
        mensagemErro = 'Servidor indisponível. Tente novamente.';
      }
    }

    this.estadoAutenticacao.update(estado => ({
      ...estado,
      carregando: false,
      erro: mensagemErro,
    }));

    return throwError(() => new Error(mensagemErro));
  }

  private definirCarregando(carregando: boolean): void {
    this.estadoAutenticacao.update(estado => ({ ...estado, carregando }));
  }

  private limparErro(): void {
    this.estadoAutenticacao.update(estado => ({ ...estado, erro: null }));
  }

  private decodificarToken(token: string): JwtPayload {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
    return JSON.parse(jsonPayload);
  }

  private payloadParaUsuario(payload: JwtPayload): Usuario {
    return {
      id: parseInt(payload.Id),
      nome: payload.Nome,
      email: payload.Email,
      papel: ehPapelValido(payload.Perfil) ? payload.Perfil : Papel.MORADOR,
      foto: payload.Foto,
    };
  }
}
