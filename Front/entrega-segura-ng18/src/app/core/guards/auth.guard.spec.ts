import { TestBed } from '@angular/core/testing';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Component } from '@angular/core';

import { authGuard } from './auth.guard';
import { AuthService } from '@core/services/auth.service';

// Mock component para testes de roteamento
@Component({ template: '' })
class MockComponent {}

describe('authGuard', () => {
  let authService: jest.Mocked<AuthService>;
  let router: jest.Mocked<Router>;
  let mockRoute: ActivatedRouteSnapshot;
  let mockState: RouterStateSnapshot;

  beforeEach(() => {
    const authServiceMock = {
      isAuthenticated: jest.fn(),
    };

    const routerMock = {
      navigate: jest.fn(),
    };

    TestBed.configureTestingModule({
      declarations: [MockComponent],
      providers: [
        { provide: AuthService, useValue: authServiceMock },
        { provide: Router, useValue: routerMock },
      ],
    });

    authService = TestBed.inject(AuthService) as jest.Mocked<AuthService>;
    router = TestBed.inject(Router) as jest.Mocked<Router>;

    // Mock ActivatedRouteSnapshot
    mockRoute = {
      params: {},
      queryParams: {},
      data: {},
      url: [],
      outlet: 'primary',
      component: MockComponent,
      routeConfig: null,
      root: {} as ActivatedRouteSnapshot,
      parent: null,
      firstChild: null,
      children: [],
      pathFromRoot: [],
      paramMap: {
        get: jest.fn(),
        getAll: jest.fn(),
        has: jest.fn(),
        keys: [],
      },
      queryParamMap: {
        get: jest.fn(),
        getAll: jest.fn(),
        has: jest.fn(),
        keys: [],
      },
      fragment: null,
      title: undefined,
    };

    // Mock RouterStateSnapshot
    mockState = {
      url: '/dashboard',
      root: {} as ActivatedRouteSnapshot,
    };

    authService.isAuthenticated.mockClear();
    router.navigate.mockClear();
  });

  describe('usuário autenticado', () => {
    beforeEach(() => {
      authService.isAuthenticated.mockReturnValue(true);
    });

    it('deve permitir acesso quando usuário está autenticado', () => {
      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso a rotas protegidas', () => {
      mockState.url = '/admin/users';

      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso a rotas com parâmetros', () => {
      mockState.url = '/entregas/123/edit';

      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });
  });

  describe('usuário não autenticado', () => {
    beforeEach(() => {
      authService.isAuthenticated.mockReturnValue(false);
    });

    it('deve negar acesso e redirecionar para login', () => {
      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/dashboard' },
      });
    });

    it('deve preservar URL de destino no returnUrl', () => {
      mockState.url = '/admin/settings';

      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/admin/settings' },
      });
    });

    it('deve lidar com URLs complexas com query params', () => {
      mockState.url = '/entregas?status=pendente&page=2';

      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/entregas?status=pendente&page=2' },
      });
    });

    it('deve lidar com URLs com fragmentos', () => {
      mockState.url = '/dashboard#section-reports';

      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/dashboard#section-reports' },
      });
    });

    it('deve lidar com URL raiz', () => {
      mockState.url = '/';

      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/' },
      });
    });
  });

  describe('edge cases', () => {
    it('deve lidar com authService retornando undefined', () => {
      authService.isAuthenticated.mockReturnValue(undefined as any);

      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/dashboard' },
      });
    });

    it('deve lidar com URL vazia', () => {
      mockState.url = '';

      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      authService.isAuthenticated.mockReturnValue(false);

      const result2 = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(result2).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '' },
      });
    });

    it('deve chamar isAuthenticated uma vez por execução', () => {
      authService.isAuthenticated.mockReturnValue(true);

      const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

      expect(authService.isAuthenticated).toHaveBeenCalledTimes(1);
      expect(result).toBe(true);
    });

    it('deve funcionar com diferentes tipos de rotas', () => {
      const testCases = [
        '/dashboard',
        '/admin/users',
        '/entregas/create',
        '/profile/settings',
        '/condominios/123/entregas',
      ];

      authService.isAuthenticated.mockReturnValue(true);

      testCases.forEach(url => {
        mockState.url = url;
        authService.isAuthenticated.mockClear();

        const result = TestBed.runInInjectionContext(() => authGuard(mockRoute, mockState));

        expect(result).toBe(true);
        expect(authService.isAuthenticated).toHaveBeenCalledTimes(1);
      });
    });
  });

  describe('integração com injeção de dependências', () => {
    it('deve injetar AuthService corretamente', () => {
      authService.isAuthenticated.mockReturnValue(true);

      TestBed.runInInjectionContext(() => {
        const injectedAuthService = TestBed.inject(AuthService);
        expect(injectedAuthService).toBe(authService);

        const result = authGuard(mockRoute, mockState);
        expect(result).toBe(true);
      });
    });

    it('deve injetar Router corretamente', () => {
      authService.isAuthenticated.mockReturnValue(false);

      TestBed.runInInjectionContext(() => {
        const injectedRouter = TestBed.inject(Router);
        expect(injectedRouter).toBe(router);

        const result = authGuard(mockRoute, mockState);
        expect(result).toBe(false);
      });
    });
  });
});
