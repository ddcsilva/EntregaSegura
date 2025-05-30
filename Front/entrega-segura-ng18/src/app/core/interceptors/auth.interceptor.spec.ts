import { TestBed } from '@angular/core/testing';
import { HttpRequest, HttpResponse, HttpErrorResponse, HttpHeaders, HttpContext } from '@angular/common/http';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

import { authInterceptor } from './auth.interceptor';
import { AutenticacaoService } from '@core/services/autenticacao.service';
import { TokenStorageService } from '@core/services/token-storage.service';

describe('authInterceptor', () => {
  let autenticacaoService: jest.Mocked<AutenticacaoService>;
  let tokenStorage: jest.Mocked<TokenStorageService>;

  const mockNext = jest.fn();
  const validToken = 'valid.jwt.token';

  beforeEach(() => {
    const autenticacaoServiceMock = {
      logout: jest.fn(),
    };

    const tokenStorageMock = {
      obterToken: jest.fn(),
      verificarTokenExpirado: jest.fn(),
    };

    const routerMock = {
      navigate: jest.fn(),
    };

    TestBed.configureTestingModule({
      providers: [
        { provide: AutenticacaoService, useValue: autenticacaoServiceMock },
        { provide: TokenStorageService, useValue: tokenStorageMock },
        { provide: Router, useValue: routerMock },
      ],
    });

    autenticacaoService = TestBed.inject(AutenticacaoService) as jest.Mocked<AutenticacaoService>;
    tokenStorage = TestBed.inject(TokenStorageService) as jest.Mocked<TokenStorageService>;

    // Reset mocks
    autenticacaoService.logout.mockClear();
    tokenStorage.obterToken.mockClear();
    tokenStorage.verificarTokenExpirado.mockClear();
    mockNext.mockClear();
  });

  describe('requests with authentication headers', () => {
    beforeEach(() => {
      tokenStorage.obterToken.mockReturnValue(validToken);
      tokenStorage.verificarTokenExpirado.mockReturnValue(false);
    });

    it('deve adicionar Authorization header quando token é válido', done => {
      const req = new HttpRequest('GET', 'http://localhost:3000/api/entregas');
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockImplementation((authReq: HttpRequest<unknown>) => {
        expect(authReq.headers.get('Authorization')).toBe(`Bearer ${validToken}`);
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(tokenStorage.obterToken).toHaveBeenCalled();
            expect(tokenStorage.verificarTokenExpirado).toHaveBeenCalledWith(validToken);
            done();
          },
        });
      });
    });

    it('deve clonar a requisição original com headers de autorização', done => {
      const originalHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
      const req = new HttpRequest('POST', 'http://localhost:3000/api/entregas', {}, { headers: originalHeaders });
      const expectedResponse = new HttpResponse({ status: 201 });

      mockNext.mockImplementation((authReq: HttpRequest<unknown>) => {
        expect(authReq.headers.get('Authorization')).toBe(`Bearer ${validToken}`);
        expect(authReq.headers.get('Content-Type')).toBe('application/json');
        expect(authReq.url).toBe(req.url);
        expect(authReq.method).toBe(req.method);
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: () => done(),
        });
      });
    });

    it('deve preservar parâmetros da query na URL', done => {
      const req = new HttpRequest('GET', 'http://localhost:3000/api/entregas?status=pendente&page=2');
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockImplementation((authReq: HttpRequest<unknown>) => {
        expect(authReq.url).toBe('http://localhost:3000/api/entregas?status=pendente&page=2');
        expect(authReq.headers.get('Authorization')).toBe(`Bearer ${validToken}`);
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: () => done(),
        });
      });
    });
  });

  describe('requests without authentication', () => {
    it('deve proceder sem adicionar header quando token não existe', done => {
      tokenStorage.obterToken.mockReturnValue(null);
      const req = new HttpRequest('GET', 'http://localhost:3000/api/entregas');
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockImplementation((authReq: HttpRequest<unknown>) => {
        expect(authReq.headers.get('Authorization')).toBeNull();
        expect(authReq).toBe(req); // Should be the original request
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(tokenStorage.obterToken).toHaveBeenCalled();
            expect(tokenStorage.verificarTokenExpirado).not.toHaveBeenCalled();
            done();
          },
        });
      });
    });

    it('deve proceder sem header quando token está expirado', done => {
      tokenStorage.obterToken.mockReturnValue(validToken);
      tokenStorage.verificarTokenExpirado.mockReturnValue(true);
      const req = new HttpRequest('GET', 'http://localhost:3000/api/entregas');
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockImplementation((authReq: HttpRequest<unknown>) => {
        expect(authReq.headers.get('Authorization')).toBeNull();
        expect(authReq).toBe(req); // Should be the original request
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(tokenStorage.obterToken).toHaveBeenCalled();
            expect(tokenStorage.verificarTokenExpirado).toHaveBeenCalledWith(validToken);
            done();
          },
        });
      });
    });
  });

  describe('response handling', () => {
    it('deve propagar resposta de sucesso normalmente', done => {
      tokenStorage.obterToken.mockReturnValue(validToken);
      tokenStorage.verificarTokenExpirado.mockReturnValue(false);
      const req = new HttpRequest('GET', 'http://localhost:3000/api/entregas');
      const successResponse = new HttpResponse({
        status: 200,
        body: { data: 'test data' },
      });

      mockNext.mockReturnValue(of(successResponse));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(successResponse);
            done();
          },
        });
      });
    });

    it('deve chamar logout quando resposta é 401 Unauthorized', done => {
      tokenStorage.obterToken.mockReturnValue(validToken);
      tokenStorage.verificarTokenExpirado.mockReturnValue(false);
      const req = new HttpRequest('GET', 'http://localhost:3000/api/entregas');
      const unauthorizedError = new HttpErrorResponse({
        status: 401,
        statusText: 'Unauthorized',
      });

      mockNext.mockReturnValue(throwError(() => unauthorizedError));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          error: error => {
            expect(error).toBe(unauthorizedError);
            expect(autenticacaoService.logout).toHaveBeenCalled();
            done();
          },
        });
      });
    });

    it('deve propagar outros erros HTTP sem fazer logout', done => {
      tokenStorage.obterToken.mockReturnValue(validToken);
      tokenStorage.verificarTokenExpirado.mockReturnValue(false);
      const req = new HttpRequest('GET', 'http://localhost:3000/api/entregas');
      const serverError = new HttpErrorResponse({
        status: 500,
        statusText: 'Internal Server Error',
      });

      mockNext.mockReturnValue(throwError(() => serverError));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          error: error => {
            expect(error).toBe(serverError);
            expect(autenticacaoService.logout).not.toHaveBeenCalled();
            done();
          },
        });
      });
    });

    it('deve lidar com erro 403 Forbidden sem fazer logout', done => {
      tokenStorage.obterToken.mockReturnValue(validToken);
      tokenStorage.verificarTokenExpirado.mockReturnValue(false);
      const req = new HttpRequest('GET', 'http://localhost:3000/api/admin');
      const forbiddenError = new HttpErrorResponse({
        status: 403,
        statusText: 'Forbidden',
      });

      mockNext.mockReturnValue(throwError(() => forbiddenError));

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          error: error => {
            expect(error).toBe(forbiddenError);
            expect(autenticacaoService.logout).not.toHaveBeenCalled();
            done();
          },
        });
      });
    });
  });

  describe('edge cases', () => {
    it('deve lidar com falha na obtenção do token', done => {
      tokenStorage.obterToken.mockImplementation(() => {
        throw new Error('localStorage not available');
      });
      const req = new HttpRequest('GET', 'http://localhost:3000/api/entregas');
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockImplementation((authReq: HttpRequest<unknown>) => {
        expect(authReq.headers.get('Authorization')).toBeNull();
        expect(authReq).toBe(req);
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(tokenStorage.obterToken).toHaveBeenCalled();
            done();
          },
        });
      });
    });

    it('deve lidar com falha na verificação de expiração do token', done => {
      tokenStorage.obterToken.mockReturnValue(validToken);
      tokenStorage.verificarTokenExpirado.mockImplementation(() => {
        throw new Error('Invalid token format');
      });
      const req = new HttpRequest('GET', 'http://localhost:3000/api/entregas');
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockImplementation((authReq: HttpRequest<unknown>) => {
        expect(authReq.headers.get('Authorization')).toBeNull();
        expect(authReq).toBe(req);
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: response => {
            expect(response).toBe(expectedResponse);
            expect(tokenStorage.obterToken).toHaveBeenCalled();
            expect(tokenStorage.verificarTokenExpirado).toHaveBeenCalledWith(validToken);
            done();
          },
        });
      });
    });

    it('deve preservar contexto da requisição original', done => {
      tokenStorage.obterToken.mockReturnValue(validToken);
      tokenStorage.verificarTokenExpirado.mockReturnValue(false);
      const httpContext = new HttpContext();
      const req = new HttpRequest(
        'PUT',
        'http://localhost:3000/api/entregas/123',
        {},
        {
          context: httpContext,
        }
      );
      const expectedResponse = new HttpResponse({ status: 200 });

      mockNext.mockImplementation((authReq: HttpRequest<unknown>) => {
        expect(authReq.context).toBe(httpContext);
        return of(expectedResponse);
      });

      TestBed.runInInjectionContext(() => {
        authInterceptor(req, mockNext).subscribe({
          next: () => done(),
        });
      });
    });
  });
});
