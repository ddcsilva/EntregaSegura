import { TestBed } from '@angular/core/testing';
import { HttpRequest, HttpResponse, HttpErrorResponse, HttpStatusCode, HttpHeaders } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

import { authInterceptor } from './auth.interceptor';
import { AuthService } from '@core/services/auth.service';
import { TokenStorageService } from '@core/services/token-storage.service';

describe('authInterceptor', () => {
  let authService: jest.Mocked<AuthService>;
  let tokenStorage: jest.Mocked<TokenStorageService>;
  let router: jest.Mocked<Router>;

  const mockNext = jest.fn();
  const validToken = 'valid.jwt.token';

  beforeEach(() => {
    const authServiceMock = {
      logout: jest.fn(),
    };

    const tokenStorageMock = {
      getToken: jest.fn(),
      isTokenExpired: jest.fn(),
      setToken: jest.fn(),
      removeToken: jest.fn(),
    };

    const routerMock = {
      navigate: jest.fn(),
    };

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        { provide: AuthService, useValue: authServiceMock },
        { provide: TokenStorageService, useValue: tokenStorageMock },
        { provide: Router, useValue: routerMock },
      ],
    });

    authService = TestBed.inject(AuthService) as jest.Mocked<AuthService>;
    tokenStorage = TestBed.inject(TokenStorageService) as jest.Mocked<TokenStorageService>;
    router = TestBed.inject(Router) as jest.Mocked<Router>;

    mockNext.mockClear();
    authService.logout.mockClear();
    tokenStorage.getToken.mockClear();
    tokenStorage.isTokenExpired.mockClear();
  });

  describe('URLs públicas', () => {
    it('deve passar requisição sem token para URL de autenticação', done => {
      const req = new HttpRequest('POST', 'http://localhost:3000/api/auth/autenticacao', {});
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockReturnValue(of(expectedResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(mockNext).toHaveBeenCalledWith(req);
            done();
          },
        });
      });
    });

    it('deve passar requisição sem token para URL pública', done => {
      const req = new HttpRequest('GET', 'http://localhost:3000/api/public/info', {});
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockReturnValue(of(expectedResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(mockNext).toHaveBeenCalledWith(req);
            done();
          },
        });
      });
    });
  });

  describe('URLs privadas com token válido', () => {
    beforeEach(() => {
      tokenStorage.getToken.mockReturnValue(validToken);
      tokenStorage.isTokenExpired.mockReturnValue(false);
    });

    it('deve adicionar token de autorização na requisição', done => {
      const req = new HttpRequest('GET', 'http://localhost:3000/api/users', {});
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockImplementation((authReq: HttpRequest<any>) => {
        expect(authReq.headers.get('Authorization')).toBe(`Bearer ${validToken}`);
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(tokenStorage.getToken).toHaveBeenCalled();
            expect(tokenStorage.isTokenExpired).toHaveBeenCalledWith(validToken);
            done();
          },
        });
      });
    });

    it('deve clonar a requisição original com headers de autorização', done => {
      const originalHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
      const req = new HttpRequest('POST', 'http://localhost:3000/api/entregas', {}, { headers: originalHeaders });
      const expectedResponse = new HttpResponse({ status: 201 });

      mockNext.mockImplementation((authReq: HttpRequest<any>) => {
        expect(authReq.headers.get('Authorization')).toBe(`Bearer ${validToken}`);
        expect(authReq.headers.get('Content-Type')).toBe('application/json');
        expect(authReq.url).toBe(req.url);
        expect(authReq.method).toBe(req.method);
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            done();
          },
        });
      });
    });
  });

  describe('URLs privadas sem token ou token expirado', () => {
    it('deve passar requisição sem modificação quando não há token', done => {
      tokenStorage.getToken.mockReturnValue(null);

      const req = new HttpRequest('GET', 'http://localhost:3000/api/users', {});
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockReturnValue(of(expectedResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(mockNext).toHaveBeenCalledWith(req);
            expect(tokenStorage.getToken).toHaveBeenCalled();
            expect(tokenStorage.isTokenExpired).not.toHaveBeenCalled();
            done();
          },
        });
      });
    });

    it('deve passar requisição sem modificação quando token está expirado', done => {
      tokenStorage.getToken.mockReturnValue(validToken);
      tokenStorage.isTokenExpired.mockReturnValue(true);

      const req = new HttpRequest('GET', 'http://localhost:3000/api/users', {});
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockReturnValue(of(expectedResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(mockNext).toHaveBeenCalledWith(req);
            expect(tokenStorage.getToken).toHaveBeenCalled();
            expect(tokenStorage.isTokenExpired).toHaveBeenCalledWith(validToken);
            done();
          },
        });
      });
    });
  });

  describe('tratamento de erros', () => {
    beforeEach(() => {
      tokenStorage.getToken.mockReturnValue(validToken);
      tokenStorage.isTokenExpired.mockReturnValue(false);
    });

    it('deve chamar logout quando receber erro 401', done => {
      const req = new HttpRequest('GET', 'http://localhost:3000/api/users', {});
      const errorResponse = new HttpErrorResponse({
        status: 401,
        statusText: 'Unauthorized',
        error: { message: 'Token inválido' },
      });

      mockNext.mockImplementation(() => throwError(() => errorResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          error: error => {
            expect(error).toBe(errorResponse);
            expect(authService.logout).toHaveBeenCalled();
            done();
          },
        });
      });
    });

    it('não deve chamar logout para outros erros HTTP', done => {
      const req = new HttpRequest('GET', 'http://localhost:3000/api/users', {});
      const errorResponse = new HttpErrorResponse({
        status: 500,
        statusText: 'Internal Server Error',
        error: { message: 'Erro interno do servidor' },
      });

      mockNext.mockImplementation(() => throwError(() => errorResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          error: error => {
            expect(error).toBe(errorResponse);
            expect(authService.logout).not.toHaveBeenCalled();
            done();
          },
        });
      });
    });

    it('deve propagar erro 403 sem fazer logout', done => {
      const req = new HttpRequest('GET', 'http://localhost:3000/api/admin', {});
      const errorResponse = new HttpErrorResponse({
        status: 403,
        statusText: 'Forbidden',
        error: { message: 'Acesso negado' },
      });

      mockNext.mockImplementation(() => throwError(() => errorResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          error: error => {
            expect(error).toBe(errorResponse);
            expect(authService.logout).not.toHaveBeenCalled();
            done();
          },
        });
      });
    });

    it('deve propagar erro 404 sem fazer logout', done => {
      const req = new HttpRequest('GET', 'http://localhost:3000/api/notfound', {});
      const errorResponse = new HttpErrorResponse({
        status: 404,
        statusText: 'Not Found',
        error: { message: 'Recurso não encontrado' },
      });

      mockNext.mockImplementation(() => throwError(() => errorResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          error: error => {
            expect(error).toBe(errorResponse);
            expect(authService.logout).not.toHaveBeenCalled();
            done();
          },
        });
      });
    });
  });

  describe('edge cases', () => {
    it('deve lidar com URL que contém parte da URL pública', done => {
      const req = new HttpRequest('GET', 'http://localhost:3000/api/auth-service/test', {});
      const expectedResponse = new HttpResponse({ status: 200 });

      tokenStorage.getToken.mockReturnValue(validToken);
      tokenStorage.isTokenExpired.mockReturnValue(false);

      mockNext.mockImplementation((authReq: HttpRequest<any>) => {
        expect(authReq.headers.get('Authorization')).toBe(`Bearer ${validToken}`);
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            done();
          },
        });
      });
    });

    it('deve identificar corretamente URL de autenticação com query params', done => {
      const req = new HttpRequest('POST', 'http://localhost:3000/api/auth/autenticacao?redirect=dashboard', {});
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockReturnValue(of(expectedResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(mockNext).toHaveBeenCalledWith(req);
            done();
          },
        });
      });
    });

    it('deve funcionar com diferentes métodos HTTP', done => {
      const methods = ['GET', 'POST', 'PUT', 'DELETE', 'PATCH'];
      let completedRequests = 0;

      tokenStorage.getToken.mockReturnValue(validToken);
      tokenStorage.isTokenExpired.mockReturnValue(false);

      methods.forEach(method => {
        const req = new HttpRequest(method, 'http://localhost:3000/api/test', {});
        const expectedResponse = new HttpResponse({ status: 200 });

        mockNext.mockImplementation((authReq: HttpRequest<any>) => {
          expect(authReq.headers.get('Authorization')).toBe(`Bearer ${validToken}`);
          expect(authReq.method).toBe(method);
          return of(expectedResponse);
        });

        TestBed.runInInjectionContext(() => {
          authInterceptor(req, mockNext).subscribe({
            next: () => {
              completedRequests++;
              if (completedRequests === methods.length) {
                done();
              }
            },
          });
        });
      });
    });
  });
});
