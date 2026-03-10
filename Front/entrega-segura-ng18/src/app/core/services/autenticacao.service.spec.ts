import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Router } from '@angular/router';

import { AutenticacaoService } from './autenticacao.service';
import { TokenStorageService } from './token-storage.service';
import { Autenticacao, LoginRequest, Papel } from '@core/models';

describe('AutenticacaoService', () => {
  let service: AutenticacaoService;
  let httpMock: HttpTestingController;
  let tokenStorage: jest.Mocked<TokenStorageService>;
  let router: jest.Mocked<Router>;

  const mockUser = {
    id: 1,
    nome: 'João Silva',
    email: 'joao@example.com',
    papel: Papel.ADMIN,
    foto: 'avatar.jpg',
  };

  const mockLoginResponse: Autenticacao = {
    token: 'jwt.token.here',
    usuario: mockUser,
  };

  const mockCredentials: LoginRequest = {
    login: 'joao@example.com',
    senha: 'senha123',
  };

  beforeEach(() => {
    const tokenStorageMock = {
      obterToken: jest.fn(),
      salvarToken: jest.fn(),
      removerToken: jest.fn(),
      verificarTokenExpirado: jest.fn(),
    };

    const routerMock = {
      navigate: jest.fn(),
    };

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AutenticacaoService,
        { provide: TokenStorageService, useValue: tokenStorageMock },
        { provide: Router, useValue: routerMock },
      ],
    });

    service = TestBed.inject(AutenticacaoService);
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

  describe('computed signals', () => {
    it('deve computar usuario corretamente', () => {
      expect(service.usuario()).toBeNull();
    });

    it('deve computar token corretamente', () => {
      expect(service.token()).toBeNull();
    });

    it('deve computar carregando corretamente', () => {
      expect(service.carregando()).toBe(false);
    });

    it('deve computar erro corretamente', () => {
      expect(service.erro()).toBeNull();
    });

    it('deve computar estaAutenticado corretamente com token válido', () => {
      tokenStorage.verificarTokenExpirado.mockReturnValue(false);

      service.login(mockCredentials).subscribe();

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush(mockLoginResponse);

      expect(service.estaAutenticado()).toBe(true);
    });

    it('deve computar papel corretamente', () => {
      service.login(mockCredentials).subscribe();

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush(mockLoginResponse);

      expect(service.papel()).toBe(Papel.ADMIN);
    });
  });

  describe('computed signals extras', () => {
    it('deve computar podeAcessarAdmin com usuário admin', () => {
      service.login(mockCredentials).subscribe();

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush(mockLoginResponse);

      expect(service.podeAcessarAdmin()).toBe(true);
    });

    it('deve computar podeAcessarAdmin com usuário não admin', () => {
      const nonAdminResponse = {
        ...mockLoginResponse,
        usuario: { ...mockUser, papel: Papel.SINDICO },
      };

      service.login(mockCredentials).subscribe();

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush(nonAdminResponse);

      expect(service.podeAcessarAdmin()).toBe(false);
    });

    it('deve computar podeAcessarSindico com sindico', () => {
      const sindicoResponse = {
        ...mockLoginResponse,
        usuario: { ...mockUser, papel: Papel.SINDICO },
      };

      service.login(mockCredentials).subscribe();

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush(sindicoResponse);

      expect(service.podeAcessarSindico()).toBe(true);
    });

    it('deve computar podeAcessarSindico com admin', () => {
      service.login(mockCredentials).subscribe();

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush(mockLoginResponse);

      expect(service.podeAcessarSindico()).toBe(true);
    });

    it('deve computar podeAcessarSindico com usuário sem permissão', () => {
      const funcionarioResponse = {
        ...mockLoginResponse,
        usuario: { ...mockUser, papel: Papel.FUNCIONARIO },
      };

      service.login(mockCredentials).subscribe();

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush(funcionarioResponse);

      expect(service.podeAcessarSindico()).toBe(false);
    });
  });

  describe('login', () => {
    it('deve fazer login com sucesso', done => {
      service.login(mockCredentials).subscribe({
        next: response => {
          expect(response).toEqual(mockLoginResponse);
          expect(service.usuario()).toEqual(mockUser);
          expect(service.token()).toBe(mockLoginResponse.token);
          expect(service.carregando()).toBe(false);
          expect(service.erro()).toBeNull();
          expect(tokenStorage.salvarToken).toHaveBeenCalledWith(mockLoginResponse.token);
          done();
        },
      });

      const req = httpMock.expectOne(
        request => request.url.includes('/api/usuario/autenticacao') && request.method === 'POST'
      );
      expect(req.request.body).toEqual(mockCredentials);
      req.flush(mockLoginResponse);
    });

    it('deve definir carregando como true durante o login', () => {
      service.login(mockCredentials).subscribe();

      expect(service.carregando()).toBe(true);

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush(mockLoginResponse);

      expect(service.carregando()).toBe(false);
    });

    it('deve lidar com erro de login', done => {
      service.login(mockCredentials).subscribe({
        error: () => {
          expect(service.erro()).toBe('Credenciais inválidas');
          expect(service.carregando()).toBe(false);
          done();
        },
      });

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush({ message: 'Credenciais inválidas' }, { status: 401, statusText: 'Unauthorized' });
    });
  });

  describe('logout', () => {
    it('deve fazer logout corretamente', () => {
      // Primeiro fazer login
      service.login(mockCredentials).subscribe();

      const loginReq = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      loginReq.flush(mockLoginResponse);

      // Depois fazer logout
      service.logout();

      expect(service.usuario()).toBeNull();
      expect(service.token()).toBeNull();
      expect(service.carregando()).toBe(false);
      expect(service.erro()).toBeNull();
      expect(tokenStorage.removerToken).toHaveBeenCalled();
      expect(router.navigate).toHaveBeenCalledWith(['/autenticacao/login']);
    });
  });

  describe('private methods', () => {
    it('deve limpar erro', () => {
      // Criar um erro primeiro
      service.login({ login: 'wrong', senha: 'wrong' }).subscribe({
        error: () => {
          // Error handler intencional para capturar erro de login
        },
      });

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush({ message: 'Erro' }, { status: 401, statusText: 'Unauthorized' });

      expect(service.erro()).toBeTruthy();

      // Tentar login novamente para acionar limparErro
      service.login(mockCredentials).subscribe();

      expect(service.erro()).toBeNull();

      const newReq = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      newReq.flush(mockLoginResponse);
    });
  });

  describe('edge cases', () => {
    it('deve manter estado consistente após erro de rede', done => {
      service.login({ login: 'test', senha: 'test' }).subscribe({
        error: () => {
          expect(service.carregando()).toBe(false);
          expect(service.erro()).toBeTruthy();
          expect(service.usuario()).toBeNull();
          expect(service.token()).toBeNull();
          done();
        },
      });

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.error(new ErrorEvent('Network error'));
    });

    it('deve lidar com resposta de login incompleta', done => {
      service.login(mockCredentials).subscribe({
        next: response => {
          expect(response.token).toBe('token');
          expect(service.usuario()).toBeTruthy();
          done();
        },
      });

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush({ token: 'token', usuario: mockUser });
    });

    it('deve lidar com token inválido na resposta', done => {
      service.login(mockCredentials).subscribe({
        next: response => {
          expect(response.token).toBe('');
          expect(service.token()).toBe('');
          done();
        },
      });

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush({ token: '', usuario: mockUser });
    });

    it('deve lidar com user inválido na resposta', done => {
      service.login(mockCredentials).subscribe({
        next: response => {
          expect(response.usuario).toBeNull();
          expect(service.usuario()).toBeNull();
          done();
        },
      });

      const req = httpMock.expectOne(request => request.url.includes('/api/usuario/autenticacao'));
      req.flush({ token: 'token', usuario: null });
    });
  });
});
