import { TestBed } from '@angular/core/testing';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Component } from '@angular/core';

import { papelGuard } from './papel.guard';
import { AuthService } from '@core/services/auth.service';
import { UserRole } from '@core/models/user.model';

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
      const guard = papelGuard([UserRole.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).not.toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/auth/login']);
    });

    it('deve redirecionar para login independente dos roles permitidos', () => {
      const testCases = [
        [UserRole.ADMIN],
        [UserRole.SINDICO],
        [UserRole.ADMIN, UserRole.SINDICO],
        [UserRole.FUNCIONARIO],
        [UserRole.ADMIN, UserRole.SINDICO, UserRole.FUNCIONARIO],
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
      authService.userRole.mockReturnValue(UserRole.ADMIN);
      const guard = papelGuard([UserRole.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso para Sindico', () => {
      authService.userRole.mockReturnValue(UserRole.SINDICO);
      const guard = papelGuard([UserRole.SINDICO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso quando usuário tem um dos roles permitidos', () => {
      authService.userRole.mockReturnValue(UserRole.SINDICO);
      const guard = papelGuard([UserRole.ADMIN, UserRole.SINDICO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
      expect(router.navigate).not.toHaveBeenCalled();
    });

    it('deve permitir acesso com múltiplos roles válidos', () => {
      const testCases = [
        { userRole: UserRole.ADMIN, allowedRoles: [UserRole.ADMIN, UserRole.SINDICO, UserRole.FUNCIONARIO] },
        { userRole: UserRole.SINDICO, allowedRoles: [UserRole.ADMIN, UserRole.SINDICO] },
        { userRole: UserRole.FUNCIONARIO, allowedRoles: [UserRole.FUNCIONARIO] },
        { userRole: UserRole.ADMIN, allowedRoles: [UserRole.ADMIN] },
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
      authService.userRole.mockReturnValue(UserRole.FUNCIONARIO);
      const guard = papelGuard([UserRole.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve negar acesso quando role não está na lista permitida', () => {
      authService.userRole.mockReturnValue(UserRole.SINDICO);
      const guard = papelGuard([UserRole.ADMIN, UserRole.FUNCIONARIO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(authService.isAuthenticated).toHaveBeenCalled();
      expect(authService.userRole).toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve lidar com diferentes combinações de roles negados', () => {
      const testCases = [
        { userRole: UserRole.FUNCIONARIO, allowedRoles: [UserRole.ADMIN] },
        { userRole: UserRole.SINDICO, allowedRoles: [UserRole.ADMIN] },
        { userRole: UserRole.FUNCIONARIO, allowedRoles: [UserRole.ADMIN, UserRole.SINDICO] },
        { userRole: UserRole.ADMIN, allowedRoles: [UserRole.SINDICO, UserRole.FUNCIONARIO] },
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
      const guard = papelGuard([UserRole.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve negar acesso quando userRole retorna undefined', () => {
      authService.userRole.mockReturnValue(null);
      const guard = papelGuard([UserRole.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve negar acesso quando allowedRoles está vazio', () => {
      authService.userRole.mockReturnValue(UserRole.ADMIN);
      const guard = papelGuard([]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve ser case sensitive com roles', () => {
      authService.userRole.mockReturnValue('administrador' as UserRole); // lowercase
      const guard = papelGuard([UserRole.ADMIN]); // uppercase

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('deve lidar com espaços em branco nos roles', () => {
      authService.userRole.mockReturnValue('Administrador ' as UserRole); // com espaço
      const guard = papelGuard([UserRole.ADMIN]); // sem espaço

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });
  });

  describe('factory function', () => {
    it('deve retornar uma função guard válida', () => {
      const guard = papelGuard([UserRole.ADMIN]);

      expect(typeof guard).toBe('function');
    });

    it('deve criar guards independentes para diferentes roles', () => {
      const adminGuard = papelGuard([UserRole.ADMIN]);
      const sindicoGuard = papelGuard([UserRole.SINDICO]);

      expect(adminGuard).not.toBe(sindicoGuard);
      expect(typeof adminGuard).toBe('function');
      expect(typeof sindicoGuard).toBe('function');
    });

    it('deve manter os roles configurados na closure', () => {
      authService.isAuthenticated.mockReturnValue(true);
      authService.userRole.mockReturnValue(UserRole.ADMIN);

      const adminOnlyGuard = papelGuard([UserRole.ADMIN]);
      const sindicoOnlyGuard = papelGuard([UserRole.SINDICO]);

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
      authService.userRole.mockReturnValue(UserRole.ADMIN);

      const guard = papelGuard([UserRole.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
    });

    it('deve funcionar para rota de sindico acessível por admin e sindico', () => {
      mockState.url = '/condominios/settings';
      authService.isAuthenticated.mockReturnValue(true);
      authService.userRole.mockReturnValue(UserRole.SINDICO);

      const guard = papelGuard([UserRole.ADMIN, UserRole.SINDICO]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(true);
    });

    it('deve bloquear funcionario tentando acessar área administrativa', () => {
      mockState.url = '/admin/settings';
      authService.isAuthenticated.mockReturnValue(true);
      authService.userRole.mockReturnValue(UserRole.FUNCIONARIO);

      const guard = papelGuard([UserRole.ADMIN]);

      const result = TestBed.runInInjectionContext(() => guard(mockRoute, mockState));

      expect(result).toBe(false);
      expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    });
  });
});
