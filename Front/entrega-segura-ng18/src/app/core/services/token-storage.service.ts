import { Injectable, inject } from '@angular/core';
import { environment } from '@environments';
import { STORAGE_KEYS } from '../constants/app.constants';
import { LoggerService } from './logger.service';

interface JwtPayload {
  exp: number;
  [key: string]: unknown;
}

@Injectable({
  providedIn: 'root',
})
export class TokenStorageService {
  private readonly logger = inject(LoggerService);
  private readonly tokenKey = environment.autenticacao?.tokenKey || STORAGE_KEYS.TOKEN;

  salvarToken(token: string): void {
    try {
      localStorage.setItem(this.tokenKey, token);
      this.logger.debug('Token salvo com sucesso', undefined, 'TokenStorageService');
    } catch (error) {
      this.logger.warn('Erro ao salvar token', error, 'TokenStorageService');
    }
  }

  obterToken(): string | null {
    try {
      const token = localStorage.getItem(this.tokenKey);
      if (token) {
        this.logger.debug('Token recuperado do storage', undefined, 'TokenStorageService');
      }
      return token;
    } catch (error) {
      this.logger.warn('Erro ao recuperar token', error, 'TokenStorageService');
      return null;
    }
  }

  removerToken(): void {
    try {
      localStorage.removeItem(this.tokenKey);
      this.logger.debug('Token removido do storage', undefined, 'TokenStorageService');
    } catch (error) {
      this.logger.warn('Erro ao remover token', error, 'TokenStorageService');
    }
  }

  verificarTokenExpirado(token: string): boolean {
    try {
      const payload = this.decodificarToken(token);
      const agora = Math.floor(Date.now() / 1000);
      const expirado = payload.exp < agora;

      if (expirado) {
        this.logger.info(
          'Token expirado detectado',
          {
            exp: payload.exp,
            now: agora,
            diferenca: agora - payload.exp,
          },
          'TokenStorageService'
        );
      }

      return expirado;
    } catch (error) {
      this.logger.warn('Erro ao verificar expiração do token', error, 'TokenStorageService');
      return true; // Se não conseguir verificar, considera expirado por segurança
    }
  }

  obterTempoRestante(token: string): number {
    try {
      const payload = this.decodificarToken(token);
      const agora = Math.floor(Date.now() / 1000);
      const tempoRestante = payload.exp - agora;

      return Math.max(0, tempoRestante);
    } catch (error) {
      this.logger.warn('Erro ao calcular tempo restante do token', error, 'TokenStorageService');
      return 0;
    }
  }

  private decodificarToken(token: string): JwtPayload {
    if (!token || token.split('.').length !== 3) {
      throw new Error('Invalid token format');
    }

    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );

    const payload = JSON.parse(jsonPayload);

    if (!payload.exp || typeof payload.exp !== 'number') {
      throw new Error('Token payload missing exp field');
    }

    return payload;
  }
}
