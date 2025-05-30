import { TestBed } from '@angular/core/testing';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Component } from '@angular/core';

import { papelGuard } from './papel.guard';
import { AuthService } from '@core/services/auth.service';
import { Papel } from '@core/models/papel.model';

// Mock component para testes de roteamento
@Component({ template: '' })
class MockComponent {}

describe('papelGuard', () => {
  let authService: jest.Mocked<AuthService>;
  let router: jest.Mocked<Router>;
  let mockRoute: ActivatedRouteSnapshot;
  let mockState: RouterStateSnapshot;

  beforeEach(() => {
    const authServiceMock = {
      isAuthenticated: jest.fn(),
      userRole: jest.fn(),
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
      url: '/admin/users',
      root: {} as ActivatedRouteSnapshot,
    };

    authService.isAuthenticated.mockClear();
    authService.userRole.mockClear();
    router.navigate.mockClear();
  });

  describe('usuário não autenticado', () => {
    beforeEach(() => {
      authService.isAuthenticated.mockReturnValue(false);
    });

    it('deve negar acesso e redirecionar para login quando não autenticado', () => {
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).not.toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login']);
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
        authService.isAuthenticated.mockClear();
        router.navigate.mockClear();

        const guard = papelGuard(allowedRoles);

        const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

        expect(result).toBe(false);
        expect(router.navigate).toHaveBeenCalledWith(['/auth/login']);
      });
    });
  });

  describe('usuário autenticado com role válido', () => {
    beforeEach(() => {
      authService.isAuthenticated.mockReturnValue(true);
    });

    it('deve permitir acesso para Administrador', () => {
      authService.userRole.mockReturnValue(Papel.ADMIN);
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso para Sindico', () => {
      authService.userRole.mockReturnValue(Papel.SINDICO);
      const guard = papelGuard([Papel.SINDICO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso quando usuário tem um dos roles permitidos', () => {
      authService.userRole.mockReturnValue(Papel.SINDICO);
      const guard = papelGuard([Papel.ADMIN, Papel.SINDICO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso com múltiplos roles válidos', () => {
      const testCases = [
        { userRole: Papel.ADMIN, allowedRoles: [Papel.ADMIN, Papel.SINDICO, Papel.FUNCIONARIO] },
        { userRole: Papel.SINDICO, allowedRoles: [Papel.ADMIN, Papel.SINDICO] },
        { userRole: Papel.FUNCIONARIO, allowedRoles: [Papel.FUNCIONARIO] },
        { userRole: Papel.ADMIN, allowedRoles: [Papel.ADMIN] },
      ];

      testCases.forEach(({ userRole, allowedRoles }) => {
        authService.userRole.mockReturnValue(userRole);
        authService.isAuthenticated.mockClear();
        authService.userRole.mockClear();
        router.navigate.mockClear();

        const guard = papelGuard(allowedRoles);

        const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

        expect(result).toBe(true);
        expect(router.navigate).not.toHaveBeenCalled();
      });
    });
  });

  describe('usuário autenticado com role inválido', () => {
    beforeEach(() => {
      authService.isAuthenticated.mockReturnValue(true);
    });

    it('deve negar acesso e redirecionar para dashboard quando role não permitido', () => {
      authService.userRole.mockReturnValue(Papel.FUNCIONARIO);
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve negar acesso quando role não está na lista permitida', () => {
      authService.userRole.mockReturnValue(Papel.SINDICO);
      const guard = papelGuard([Papel.ADMIN, Papel.FUNCIONARIO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
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
        authService.userRole.mockReturnValue(userRole);
        authService.isAuthenticated.mockClear();
        authService.userRole.mockClear();
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
      authService.isAuthenticated.mockReturnValue(true);
    });

    it('deve negar acesso quando userRole retorna null', () => {
      authService.userRole.mockReturnValue(null);
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve negar acesso quando userRole retorna undefined', () => {
      authService.userRole.mockReturnValue(null);
      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve negar acesso quando allowedRoles está vazio', () => {
      authService.userRole.mockReturnValue(Papel.ADMIN);
      const guard = papelGuard([]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve ser case sensitive com roles', () => {
      authService.userRole.mockReturnValue('administrador' as Papel); // lowercase
      const guard = papelGuard([Papel.ADMIN]); // uppercase

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve lidar com espaços em branco nos roles', () => {
      authService.userRole.mockReturnValue('Administrador ' as Papel); // com espaço
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

    it('deve criar guards independentes para diferentes roles', () => {
      const adminGuard = papelGuard([Papel.ADMIN]);
      const sindicoGuard = papelGuard([Papel.SINDICO]);

      expect(adminGuard).not.toBe(sindicoGuard);
      expect(typeof adminGuard).toBe('function');
      expect(typeof sindicoGuard).toBe('function');
    });

    it('deve manter os roles configurados na closure', () => {
      authService.isAuthenticated.mockReturnValue(true);
      authService.userRole.mockReturnValue(Papel.ADMIN);

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
      mockState.url = '/admin/users';
      authService.isAuthenticated.mockReturnValue(true);
      authService.userRole.mockReturnValue(Papel.ADMIN);

      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
    });

    it('deve funcionar para rota de sindico acessível por admin e sindico', () => {
      mockState.url = '/condominios/settings';
      authService.isAuthenticated.mockReturnValue(true);
      authService.userRole.mockReturnValue(Papel.SINDICO);

      const guard = papelGuard([Papel.ADMIN, Papel.SINDICO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
    });

    it('deve bloquear funcionario tentando acessar área administrativa', () => {
      mockState.url = '/admin/settings';
      authService.isAuthenticated.mockReturnValue(true);
      authService.userRole.mockReturnValue(Papel.FUNCIONARIO);

      const guard = papelGuard([Papel.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });
  });
});
