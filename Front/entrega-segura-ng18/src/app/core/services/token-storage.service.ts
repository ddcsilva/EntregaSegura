import { Injectable } from '@angular/core';
import { environment } from '@environments';

interface JwtPayload {
  exp: number;
  [key: string]: unknown;
}

@Injectable({
  providedIn: 'root',
})
export class TokenStorageService {
  private readonly TOKEN_KEY = environment.autenticacao.tokenKey;

  salvarToken(token: string): void {
    try {
      if (this.verificarDisponibilidadeLocalStorage()) {
        localStorage.setItem(this.TOKEN_KEY, token);
      }
    } catch (error) {
      console.warn('Erro ao salvar token:', error);
    }
  }

  obterToken(): string | null {
    try {
      if (this.verificarDisponibilidadeLocalStorage()) {
        return localStorage.getItem(this.TOKEN_KEY);
      }
    } catch (error) {
      console.warn('Erro ao recuperar token:', error);
    }
    return null;
  }

  removerToken(): void {
    try {
      if (this.verificarDisponibilidadeLocalStorage()) {
        localStorage.removeItem(this.TOKEN_KEY);
      }
    } catch (error) {
      console.warn('Erro ao remover token:', error);
    }
  }

  verificarTokenExpirado(token: string): boolean {
    try {
      const payload = this.decodificarJwt(token);
      const currentTime = Math.floor(Date.now() / 1000);
      return payload.exp < currentTime;
    } catch {
      return true;
    }
  }

  private decodificarJwt(token: string): JwtPayload {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
    return JSON.parse(jsonPayload) as JwtPayload;
  }

  private verificarDisponibilidadeLocalStorage(): boolean {
    try {
      const test = '__localStorage_test__';
      localStorage.setItem(test, test);
      localStorage.removeItem(test);
      return true;
    } catch {
      return false;
    }
  }
}
