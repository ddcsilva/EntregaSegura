import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Router } from '@angular/router';

import { AuthService } from './auth.service';
import { TokenStorageService } from './token-storage.service';
import { AuthResponse, LoginRequest } from '@core/models';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;
  let tokenStorage: jest.Mocked<TokenStorageService>;
  let router: jest.Mocked<Router>;

  const mockAuthResponse: AuthResponse = {
    token: 'fake.jwt.token',
    user: {
      id: 1,
      nome: 'Test User',
      email: 'test@test.com',
      perfil: 'Administrador',
    },
  };

  beforeEach(() => {
    const tokenStorageMock = {
      setToken: jest.fn(),
      getToken: jest.fn(),
      removeToken: jest.fn(),
      isTokenExpired: jest.fn(),
    };

    const routerMock = {
      navigate: jest.fn(),
    };

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AuthService,
        { provide: TokenStorageService, useValue: tokenStorageMock },
        { provide: Router, useValue: routerMock },
      ],
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
    tokenStorage = TestBed.inject(TokenStorageService) as jest.Mocked<TokenStorageService>;
    router = TestBed.inject(Router) as jest.Mocked<Router>;
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('deve ser criado', () => {
    expect(service).toBeTruthy();
  });

  describe('login', () => {
    it('deve fazer login com sucesso e atualizar signals', done => {
      const credentials: LoginRequest = {
        login: 'test@test.com',
        senha: '123456',
      };

      service.login(credentials).subscribe({
        next: response => {
          expect(response).toEqual(mockAuthResponse);
          expect(service.user()).toEqual(mockAuthResponse.user);
          expect(service.isAuthenticated()).toBe(true);
          expect(tokenStorage.setToken).toHaveBeenCalledWith(mockAuthResponse.token);
          done();
        },
      });

      const req = httpMock.expectOne(`${service['apiUrl']}/autenticacao`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(credentials);

      req.flush(mockAuthResponse);
    });

    it('deve tratar erro de login e atualizar signal de erro', done => {
      const credentials: LoginRequest = {
        login: 'test@test.com',
        senha: 'wrong-password',
      };

      service.login(credentials).subscribe({
        error: error => {
          expect(service.error()).toBe('Credenciais inválidas');
          expect(service.isLoading()).toBe(false);
          done();
        },
      });

      const req = httpMock.expectOne(`${service['apiUrl']}/autenticacao`);
      req.flush({ message: 'Credenciais inválidas' }, { status: 401, statusText: 'Unauthorized' });
    });
  });

  describe('logout', () => {
    it('deve fazer logout e limpar estado', () => {
      // Simular estado autenticado
      service.login({ login: 'test', senha: 'test' }).subscribe();
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockAuthResponse);

      // Fazer logout
      service.logout();

      expect(tokenStorage.removeToken).toHaveBeenCalled();
      expect(service.user()).toBeNull();
      expect(service.token()).toBeNull();
      expect(service.isAuthenticated()).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login']);
    });
  });

  describe('computed signals', () => {
    it('deve computar isAuthenticated corretamente', () => {
      tokenStorage.isTokenExpired.mockReturnValue(false);

      // Estado inicial
      expect(service.isAuthenticated()).toBe(false);

      // Após login
      service.login({ login: 'test', senha: 'test' }).subscribe();
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockAuthResponse);

      expect(service.isAuthenticated()).toBe(true);
    });

    it('deve computar userRole corretamente', () => {
      expect(service.userRole()).toBeNull();

      service.login({ login: 'test', senha: 'test' }).subscribe();
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockAuthResponse);

      expect(service.userRole()).toBe('Administrador');
    });
  });
});
