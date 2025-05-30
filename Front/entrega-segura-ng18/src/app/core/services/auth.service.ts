import { Injectable, computed, signal, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, catchError, tap, throwError } from 'rxjs';

import { environment } from '@environments';
import { AuthState, LoginRequest, AuthResponse, User, JwtPayload } from '@core/models';
import { UserRole, isValidUserRole } from '@core/models';
import { TokenStorageService } from './token-storage.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);
  private readonly tokenStorage = inject(TokenStorageService);

  private readonly apiUrl = `${environment.api.baseUrl}${environment.api.endpoints.auth}`;

  private readonly authState = signal<AuthState>({
    user: null,
    token: null,
    isLoading: false,
    error: null,
  });

  public readonly user = computed(() => this.authState().user);
  public readonly token = computed(() => this.authState().token);
  public readonly isLoading = computed(() => this.authState().isLoading);
  public readonly error = computed(() => this.authState().error);

  public readonly isAuthenticated = computed(() => {
    const token = this.token();
    return !!token && !this.tokenStorage.isTokenExpired(token);
  });

  public readonly userRole = computed(() => this.user()?.perfil ?? null);

  public readonly canAccessAdmin = computed(() => this.userRole() === 'Administrador');

  public readonly canAccessSindico = computed(() => ['Administrador', 'Sindico'].includes(this.userRole() ?? ''));

  constructor() {
    this.initializeAuth();
  }

  private initializeAuth(): void {
    const storedToken = this.tokenStorage.getToken();

    if (storedToken && !this.tokenStorage.isTokenExpired(storedToken)) {
      try {
        const payload = this.decodeToken(storedToken);
        const user = this.payloadToUser(payload);

        this.authState.update(state => ({
          ...state,
          user,
          token: storedToken,
        }));
      } catch (error) {
        console.warn('Token inválido encontrado, removendo:', error);
        this.tokenStorage.removeToken();
      }
    }
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    this.setLoading(true);
    this.clearError();

    return this.http.post<AuthResponse>(`${this.apiUrl}/autenticacao`, credentials).pipe(
      tap(response => this.handleLoginSuccess(response)),
      catchError(error => this.handleAuthError(error, 'Erro ao fazer login'))
    );
  }

  logout(): void {
    this.tokenStorage.removeToken();

    this.authState.set({
      user: null,
      token: null,
      isLoading: false,
      error: null,
    });

    this.router.navigate(['/auth/login']);
  }

  private handleLoginSuccess(response: AuthResponse): void {
    const { token, user } = response;

    this.tokenStorage.setToken(token);

    this.authState.update(state => ({
      ...state,
      user,
      token,
      isLoading: false,
      error: null,
    }));
  }

  private handleAuthError(error: unknown, defaultMessage: string): Observable<never> {
    let errorMessage = defaultMessage;

    if (error instanceof HttpErrorResponse) {
      if (error.error?.message) {
        errorMessage = error.error.message;
      } else if (error.status === 401) {
        errorMessage = 'Credenciais inválidas';
      } else if (error.status === 0) {
        errorMessage = 'Servidor indisponível. Tente novamente.';
      }
    }

    this.authState.update(state => ({
      ...state,
      isLoading: false,
      error: errorMessage,
    }));

    return throwError(() => new Error(errorMessage));
  }

  private setLoading(isLoading: boolean): void {
    this.authState.update(state => ({ ...state, isLoading }));
  }

  private clearError(): void {
    this.authState.update(state => ({ ...state, error: null }));
  }

  private decodeToken(token: string): JwtPayload {
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

  private payloadToUser(payload: JwtPayload): User {
    return {
      id: parseInt(payload.Id),
      nome: payload.Nome,
      email: payload.Email,
      perfil: isValidUserRole(payload.Perfil) ? payload.Perfil : UserRole.MORADOR,
      foto: payload.Foto,
    };
  }
}
