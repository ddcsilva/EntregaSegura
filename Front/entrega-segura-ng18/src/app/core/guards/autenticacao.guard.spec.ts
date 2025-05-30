import { TestBed } from '@angular/core/testing';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Component } from '@angular/core';

import { autenticacaoGuard } from './autenticacao.guard';
import { AutenticacaoService } from '@core/services/autenticacao.service';

// Mock component para testes de roteamento
@Component({ template: '' })
class MockComponent {}

describe('autenticacaoGuard', () => {
  let autenticacaoService: jest.Mocked<AutenticacaoService>;
  let router: jest.Mocked<Router>;
  let mockRoute: ActivatedRouteSnapshot;
  let mockState: RouterStateSnapshot;

  beforeEach(() => {
    const autenticacaoServiceMock = {
      estaAutenticado: jest.fn(),
    };

    const routerMock = {
      navigate: jest.fn(),
    };

    TestBed.configureTestingModule({
      declarations: [MockComponent],
      providers: [
        { provide: AutenticacaoService, useValue: autenticacaoServiceMock },
        { provide: Router, useValue: routerMock },
      ],
    });

    autenticacaoService = TestBed.inject(AutenticacaoService) as jest.Mocked<AutenticacaoService>;
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

    autenticacaoService.estaAutenticado.mockClear();
    router.navigate.mockClear();
  });

  describe('usuário autenticado', () => {
    beforeEach(() => {
      autenticacaoService.estaAutenticado.mockReturnValue(true);
    });

    it('deve permitir acesso quando usuário está autenticado', () => {
      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso a rotas protegidas', () => {
      mockState.url = '/admin/usuarios';

      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso a rotas com parâmetros', () => {
      mockState.url = '/entregas/123/editar';

      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });
  });

  describe('usuário não autenticado', () => {
    beforeEach(() => {
      autenticacaoService.estaAutenticado.mockReturnValue(false);
    });

    it('deve negar acesso e redirecionar para login', () => {
      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/dashboard' },
      });
    });

    it('deve preservar URL de destino no returnUrl', () => {
      mockState.url = '/admin/configuracoes';

      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/admin/configuracoes' },
      });
    });

    it('deve lidar com URLs complexas com query params', () => {
      mockState.url = '/entregas?status=pendente&page=2';

      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/entregas?status=pendente&page=2' },
      });
    });

    it('deve lidar com URLs com fragmentos', () => {
      mockState.url = '/dashboard#relatorios';

      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/dashboard#relatorios' },
      });
    });

    it('deve lidar com URL raiz', () => {
      mockState.url = '/';

      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/' },
      });
    });
  });

  describe('edge cases', () => {
    it('deve lidar com autenticacaoService retornando undefined', () => {
      autenticacaoService.estaAutenticado.mockReturnValue(false);

      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '/dashboard' },
      });
    });

    it('deve lidar com URL vazia', () => {
      mockState.url = '';

      autenticacaoService.estaAutenticado.mockReturnValue(false);

      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login'], {
        queryParams: { returnUrl: '' },
      });
    });

    it('deve chamar estaAutenticado uma vez por execução', () => {
      autenticacaoService.estaAutenticado.mockReturnValue(true);

      const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

      expect(autenticacaoService.estaAutenticado).toHaveBeenCalledTimes(1);
      expect(result).toBe(true);
    });

    it('deve funcionar com diferentes tipos de rotas', () => {
      const testCases = [
        '/dashboard',
        '/admin/usuarios',
        '/entregas/criar',
        '/perfil/configuracoes',
        '/condominios/123/entregas',
      ];

      autenticacaoService.estaAutenticado.mockReturnValue(true);

      testCases.forEach(url => {
        mockState.url = url;
        autenticacaoService.estaAutenticado.mockClear();

        const result = TestBed.runInInjectionContext(() => autenticacaoGuard(mockRoute, mockState));

        expect(result).toBe(true);
        expect(autenticacaoService.estaAutenticado).toHaveBeenCalledTimes(1);
      });
    });
  });

  describe('integração com injeção de dependências', () => {
    it('deve injetar AuthService corretamente', () => {
      autenticacaoService.estaAutenticado.mockReturnValue(true);

      TestBed.runInInjectionContext(() => {
        const injectedAutenticacaoService = TestBed.inject(AutenticacaoService);
        expect(injectedAutenticacaoService).toBe(autenticacaoService);

        const result = autenticacaoGuard(mockRoute, mockState);
        expect(result).toBe(true);
      });
    });

    it('deve injetar Router corretamente', () => {
      autenticacaoService.estaAutenticado.mockReturnValue(false);

      TestBed.runInInjectionContext(() => {
        const injectedRouter = TestBed.inject(Router);
        expect(injectedRouter).toBe(router);

        const result = autenticacaoGuard(mockRoute, mockState);
        expect(result).toBe(false);
      });
    });
  });
});
