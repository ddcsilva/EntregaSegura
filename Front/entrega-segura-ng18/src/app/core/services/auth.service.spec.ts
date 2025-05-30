import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Router } from '@angular/router';

import { AuthService } from './auth.service';
import { TokenStorageService } from './token-storage.service';
import { AuthResponse, LoginRequest, UserRole } from '@core/models';

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
      perfil: UserRole.ADMIN,
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

      expect(service.userRole()).toBe(UserRole.ADMIN);
    });
  });

  describe('computed signals extras', () => {
    it('deve testar canAccessAdmin com usuário admin', () => {
      service.login({ login: 'admin', senha: 'admin' }).subscribe();
      const mockResponse = {
        ...mockAuthResponse,
        user: { ...mockAuthResponse.user, perfil: UserRole.ADMIN },
      };
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockResponse);

      expect(service.canAccessAdmin()).toBe(true);
    });

    it('deve testar canAccessAdmin com usuário não admin', () => {
      service.login({ login: 'user', senha: 'user' }).subscribe();
      const mockResponse = {
        ...mockAuthResponse,
        user: { ...mockAuthResponse.user, perfil: UserRole.SINDICO },
      };
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockResponse);

      expect(service.canAccessAdmin()).toBe(false);
    });

    it('deve testar canAccessSindico com sindico', () => {
      service.login({ login: 'sindico', senha: 'sindico' }).subscribe();
      const mockResponse = {
        ...mockAuthResponse,
        user: { ...mockAuthResponse.user, perfil: UserRole.SINDICO },
      };
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockResponse);

      expect(service.canAccessSindico()).toBe(true);
    });

    it('deve testar canAccessSindico com admin', () => {
      service.login({ login: 'admin', senha: 'admin' }).subscribe();
      const mockResponse = {
        ...mockAuthResponse,
        user: { ...mockAuthResponse.user, perfil: UserRole.ADMIN },
      };
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockResponse);

      expect(service.canAccessSindico()).toBe(true);
    });

    it('deve testar canAccessSindico com usuário sem permissão', () => {
      service.login({ login: 'user', senha: 'user' }).subscribe();
      const mockResponse = {
        ...mockAuthResponse,
        user: { ...mockAuthResponse.user, perfil: UserRole.FUNCIONARIO },
      };
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockResponse);

      expect(service.canAccessSindico()).toBe(false);
    });
  });

  describe('private methods', () => {
    it('deve testar setLoading', () => {
      service.login({ login: 'test', senha: 'test' }).subscribe();

      // Durante o login, isLoading deve ser true
      expect(service.isLoading()).toBe(true);

      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockAuthResponse);

      // Após o login, isLoading deve ser false
      expect(service.isLoading()).toBe(false);
    });

    it('deve testar clearError', () => {
      // Primeiro, criar um erro
      service.login({ login: 'wrong', senha: 'wrong' }).subscribe({
        error: () => {},
      });
      httpMock
        .expectOne(`${service['apiUrl']}/autenticacao`)
        .flush({ message: 'Erro' }, { status: 401, statusText: 'Unauthorized' });

      expect(service.error()).toBeTruthy();

      // Fazer novo login que deve limpar o erro
      service.login({ login: 'correct', senha: 'correct' }).subscribe();
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(mockAuthResponse);

      expect(service.error()).toBeNull();
    });
  });

  describe('edge cases', () => {
    it('deve manter estado consistente após erro de rede', done => {
      service.login({ login: 'test', senha: 'test' }).subscribe({
        error: error => {
          expect(service.isLoading()).toBe(false);
          expect(service.error()).toBeTruthy();
          expect(service.user()).toBeNull();
          expect(service.token()).toBeNull();
          done();
        },
      });

      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).error(new ErrorEvent('Network error'));
    });

    it('deve lidar com resposta de login incompleta', () => {
      const incompleteResponse = {
        // Resposta sem token nem user
      };

      service.login({ login: 'test', senha: 'test' }).subscribe();
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(incompleteResponse);

      // Quando a resposta não tem token nem user, o service mantém os valores undefined
      expect(service.token()).toBeUndefined();
      expect(service.user()).toBeUndefined();
      expect(service.isAuthenticated()).toBe(false);
    });

    it('deve lidar com token inválido na resposta', () => {
      const responseWithInvalidToken = {
        token: '', // token vazio
        user: mockAuthResponse.user,
      };

      service.login({ login: 'test', senha: 'test' }).subscribe();
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(responseWithInvalidToken);

      // Token vazio não é considerado válido para autenticação
      expect(service.token()).toBe('');
      expect(service.isAuthenticated()).toBe(false); // Token vazio não autentica
    });

    it('deve lidar com user inválido na resposta', () => {
      const responseWithInvalidUser = {
        token: mockAuthResponse.token,
        user: null, // user nulo
      };

      service.login({ login: 'test', senha: 'test' }).subscribe();
      httpMock.expectOne(`${service['apiUrl']}/autenticacao`).flush(responseWithInvalidUser);

      // User nulo deve ser aceito como está
      expect(service.user()).toBeNull();
      expect(service.token()).toBe(mockAuthResponse.token);
    });
  });
});
