import { TestBed } from '@angular/core/testing';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Component } from '@angular/core';

import { papelGuard } from './papel.guard';
import { AutenticacaoService } from '@core/services/autenticacao.service';
import { Papel } from '@core/models/papel.model';

// Mock component para testes de roteamento
@Component({ template: '' })
class MockComponent {}

describe('papelGuard', () => {
  let autenticacaoService: jest.Mocked<AutenticacaoService>;
  let router: jest.Mocked<Router>;
  let mockRoute: ActivatedRouteSnapshot;
  let mockState: RouterStateSnapshot;

  beforeEach(() => {
    const autenticacaoServiceMock = {
      estaAutenticado: jest.fn(),
      papel: jest.fn(),
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
      url: '/admin/users',
      root: {} as ActivatedRouteSnapshot,
    };

    autenticacaoService.estaAutenticado.mockClear();
    autenticacaoService.papel.mockClear();
    router.navigate.mockClear();
  });

  describe('usuário não autenticado', () => {
    beforeEach(() => {
      autenticacaoService.estaAutenticado.mockReturnValue(false);
    });

    it('deve negar acesso e redirecionar para login quando não autenticado', () => {
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(autenticacaoService.papel).not.toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/autenticacao/login']);
    });

    it('deve redirecionar para login independente dos roles permitidos', () => {
      const testCases = [
        [Papel.ADMIN],
        [Papel.SINDICO],
        [Papel.ADMIN, Papel.SINDICO],
        [Papel.FUNCIONARIO],
        [Papel.ADMIN, Papel.SINDICO, Papel.FUNCIONARIO],
      ];

      testCases.forEach(allowedRoles => {
        autenticacaoService.estaAutenticado.mockClear();
        router.navigate.mockClear();

        const guard = papelGuard(allowedRoles);

        const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

        expect(result).toBe(false);
        expect(router.navigate).toHaveBeenCalledWith(['/autenticacao/login']);
      });
    });
  });

  describe('usuário autenticado com role válido', () => {
    beforeEach(() => {
      autenticacaoService.estaAutenticado.mockReturnValue(true);
    });

    it('deve permitir acesso para Administrador', () => {
      autenticacaoService.papel.mockReturnValue(Papel.ADMIN);
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(autenticacaoService.papel).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso para Sindico', () => {
      autenticacaoService.papel.mockReturnValue(Papel.SINDICO);
      const guard = papelGuard([Papel.SINDICO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(autenticacaoService.papel).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso quando usuário tem um dos papéis permitidos', () => {
      autenticacaoService.papel.mockReturnValue(Papel.SINDICO);
      const guard = papelGuard([Papel.ADMIN, Papel.SINDICO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(autenticacaoService.papel).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso com múltiplos papéis válidos', () => {
      const testCases = [
        { userRole: Papel.ADMIN, allowedRoles: [Papel.ADMIN, Papel.SINDICO, Papel.FUNCIONARIO] },
        { userRole: Papel.SINDICO, allowedRoles: [Papel.ADMIN, Papel.SINDICO] },
        { userRole: Papel.FUNCIONARIO, allowedRoles: [Papel.FUNCIONARIO] },
        { userRole: Papel.ADMIN, allowedRoles: [Papel.ADMIN] },
      ];

      testCases.forEach(({ userRole, allowedRoles }) => {
        autenticacaoService.papel.mockReturnValue(userRole);
        autenticacaoService.estaAutenticado.mockClear();
        autenticacaoService.papel.mockClear();
        router.navigate.mockClear();

        const guard = papelGuard(allowedRoles);

        const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

        expect(result).toBe(true);
        expect(router.navigate).not.toHaveBeenCalled();
      });
    });
  });

  describe('usuário autenticado com papel inválido', () => {
    beforeEach(() => {
      autenticacaoService.estaAutenticado.mockReturnValue(true);
    });

    it('deve negar acesso e redirecionar para dashboard quando papel não permitido', () => {
      autenticacaoService.papel.mockReturnValue(Papel.FUNCIONARIO);
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(autenticacaoService.papel).toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve negar acesso quando papel não está na lista permitida', () => {
      autenticacaoService.papel.mockReturnValue(Papel.SINDICO);
      const guard = papelGuard([Papel.ADMIN, Papel.FUNCIONARIO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(autenticacaoService.estaAutenticado).toHaveBeenCalled();
      expect(autenticacaoService.papel).toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve lidar com diferentes combinações de roles negados', () => {
      const testCases = [
        { userRole: Papel.FUNCIONARIO, allowedRoles: [Papel.ADMIN] },
        { userRole: Papel.SINDICO, allowedRoles: [Papel.ADMIN] },
        { userRole: Papel.FUNCIONARIO, allowedRoles: [Papel.ADMIN, Papel.SINDICO] },
        { userRole: Papel.ADMIN, allowedRoles: [Papel.SINDICO, Papel.FUNCIONARIO] },
      ];

      testCases.forEach(({ userRole, allowedRoles }) => {
        autenticacaoService.papel.mockReturnValue(userRole);
        autenticacaoService.estaAutenticado.mockClear();
        autenticacaoService.papel.mockClear();
        router.navigate.mockClear();

        const guard = papelGuard(allowedRoles);

        const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

        expect(result).toBe(false);
        expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
      });
    });
  });

  describe('edge cases', () => {
    beforeEach(() => {
      autenticacaoService.estaAutenticado.mockReturnValue(true);
    });

    it('deve negar acesso quando papel retorna null', () => {
      autenticacaoService.papel.mockReturnValue(null);
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve negar acesso quando papel retorna undefined', () => {
      autenticacaoService.papel.mockReturnValue(null);
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve negar acesso quando papéis permitidos está vazio', () => {
      autenticacaoService.papel.mockReturnValue(Papel.ADMIN);
      const guard = papelGuard([]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve ser case sensitive com papéis', () => {
      autenticacaoService.papel.mockReturnValue('administrador' as Papel); // lowercase
      const guard = papelGuard([Papel.ADMIN]); // uppercase

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve lidar com espaços em branco nos papéis', () => {
      autenticacaoService.papel.mockReturnValue('Administrador ' as Papel); // com espaço
      const guard = papelGuard([Papel.ADMIN]); // sem espaço

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });
  });

  describe('factory function', () => {
    it('deve retornar uma função guard válida', () => {
      const guard = papelGuard([Papel.ADMIN]);

      expect(typeof guard).toBe('function');
    });

    it('deve criar guards independentes para diferentes papéis', () => {
      const adminGuard = papelGuard([Papel.ADMIN]);
      const sindicoGuard = papelGuard([Papel.SINDICO]);

      expect(adminGuard).not.toBe(sindicoGuard);
      expect(typeof adminGuard).toBe('function');
      expect(typeof sindicoGuard).toBe('function');
    });

    it('deve manter os papéis configurados na closure', () => {
      autenticacaoService.estaAutenticado.mockReturnValue(true);
      autenticacaoService.papel.mockReturnValue(Papel.ADMIN);

      const adminOnlyGuard = papelGuard([Papel.ADMIN]);
      const sindicoOnlyGuard = papelGuard([Papel.SINDICO]);

      const adminResult = TestBed.runInInjectionContext(() => adminOnlyGuard(mockRoute, mockState));
      const sindicoResult = TestBed.runInInjectionContext(() => sindicoOnlyGuard(mockRoute, mockState));

      expect(adminResult).toBe(true);
      expect(sindicoResult).toBe(false);
    });
  });

  describe('integração com cenários reais', () => {
    it('deve funcionar para rota de administração apenas para admins', () => {
      mockState.url = '/admin/usuarios';
      autenticacaoService.estaAutenticado.mockReturnValue(true);
      autenticacaoService.papel.mockReturnValue(Papel.ADMIN);

      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
    });

    it('deve funcionar para rota de sindico acessível por admin e sindico', () => {
      mockState.url = '/condominios/configuracoes';
      autenticacaoService.estaAutenticado.mockReturnValue(true);
      autenticacaoService.papel.mockReturnValue(Papel.SINDICO);

      const guard = papelGuard([Papel.ADMIN, Papel.SINDICO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
    });

    it('deve bloquear funcionario tentando acessar área administrativa', () => {
      mockState.url = '/admin/configuracoes';
      autenticacaoService.estaAutenticado.mockReturnValue(true);
      autenticacaoService.papel.mockReturnValue(Papel.FUNCIONARIO);

      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });
  });
});
