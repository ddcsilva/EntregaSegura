import { TestBed } from '@angular/core/testing';
import { TokenStorageService } from './token-storage.service';

describe('TokenStorageService', () => {
  let service: TokenStorageService;

  // Mock token válido (não expira até 2099)
  const validToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJleHAiOjQwOTI1NzE2MDB9.4n8a5JfK7VvEoVjWkc5jG3-ZKe3qKp1n2CjJj3TvNcw';

  // Mock token expirado (2020)
  const expiredToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJleHAiOjE1Nzc4ODAwMDB9.invalid';

  // Token malformado
  const malformedToken = 'invalid.token.here';

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TokenStorageService);

    // Limpar localStorage antes de cada teste
    localStorage.clear();
  });

  afterEach(() => {
    localStorage.clear();
    jest.restoreAllMocks();
  });

  it('deve ser criado', () => {
    expect(service).toBeTruthy();
  });

  describe('setToken', () => {
    it('deve salvar token no localStorage', () => {
      const token = 'test.token.123';

      service.setToken(token);

      expect(localStorage.getItem('entrega_segura_dev_token')).toBe(token);
    });
  });

  describe('getToken', () => {
    it('deve recuperar token do localStorage', () => {
      const token = 'test.token.123';
      localStorage.setItem('entrega_segura_dev_token', token);

      const result = service.getToken();

      expect(result).toBe(token);
    });

    it('deve retornar null quando não há token', () => {
      const result = service.getToken();

      expect(result).toBeNull();
    });
  });

  describe('removeToken', () => {
    it('deve remover token do localStorage', () => {
      localStorage.setItem('entrega_segura_dev_token', 'test-token');

      service.removeToken();

      expect(localStorage.getItem('entrega_segura_dev_token')).toBeNull();
    });
  });

  describe('isTokenExpired', () => {
    it('deve retornar false para token válido', () => {
      const result = service.isTokenExpired(validToken);

      expect(result).toBe(false);
    });

    it('deve retornar true para token expirado', () => {
      const result = service.isTokenExpired(expiredToken);

      expect(result).toBe(true);
    });

    it('deve retornar true para token malformado', () => {
      const result = service.isTokenExpired(malformedToken);

      expect(result).toBe(true);
    });

    it('deve retornar true para token vazio', () => {
      const result = service.isTokenExpired('');

      expect(result).toBe(true);
    });

    it('deve retornar true para token que não pode ser decodificado', () => {
      const result = service.isTokenExpired('invalid');

      expect(result).toBe(true);
    });
  });

  describe('private methods coverage', () => {
    it('deve verificar se localStorage está disponível (caso feliz)', () => {
      // Testamos indiretamente através dos métodos públicos
      service.setToken('test');
      const result = service.getToken();

      expect(result).toBe('test');
    });

    it('deve decodificar JWT corretamente', () => {
      // Testamos indiretamente através do isTokenExpired
      const result = service.isTokenExpired(validToken);

      expect(result).toBe(false);
    });
  });

  describe('edge cases', () => {
    it('deve lidar com token JWT sem pontos', () => {
      const result = service.isTokenExpired('tokenSemPontos');

      expect(result).toBe(true);
    });

    it('deve lidar com payload JWT inválido', () => {
      const invalidJWT = 'header.invalidBase64Payload.signature';
      const result = service.isTokenExpired(invalidJWT);

      expect(result).toBe(true);
    });

    it('deve lidar com token que tem payload que não é JSON válido', () => {
      // Token com payload que não pode ser parsed
      const invalidJsonJWT = 'header.aW52YWxpZF9qc29u.signature'; // "invalid_json" em base64
      const result = service.isTokenExpired(invalidJsonJWT);

      expect(result).toBe(true);
    });

    it('deve lidar com token sem partes suficientes', () => {
      const result = service.isTokenExpired('apenas.uma.parte.demais');

      expect(result).toBe(true);
    });
  });

  describe('error handling', () => {
    it('deve lidar com localStorage indisponível no setToken', () => {
      const consoleSpy = jest.spyOn(console, 'warn').mockImplementation();

      // Mock da verificação de localStorage
      const spy = jest.spyOn(service as any, 'isLocalStorageAvailable').mockReturnValue(false);

      service.setToken('test-token');

      // Não deve fazer nada quando localStorage não está disponível
      expect(localStorage.getItem('entrega_segura_dev_token')).toBeNull();

      spy.mockRestore();
      consoleSpy.mockRestore();
    });

    it('deve lidar com localStorage indisponível no getToken', () => {
      // Mock da verificação de localStorage
      const spy = jest.spyOn(service as any, 'isLocalStorageAvailable').mockReturnValue(false);

      const result = service.getToken();

      expect(result).toBeNull();

      spy.mockRestore();
    });

    it('deve lidar com localStorage indisponível no removeToken', () => {
      // Mock da verificação de localStorage
      const spy = jest.spyOn(service as any, 'isLocalStorageAvailable').mockReturnValue(false);

      // Não deve lançar erro
      expect(() => service.removeToken()).not.toThrow();

      spy.mockRestore();
    });
  });
});
