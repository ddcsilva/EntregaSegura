import { TestBed } from '@angular/core/testing';
import { TokenStorageService } from './token-storage.service';

describe('TokenStorageService', () => {
  let service: TokenStorageService;

  // Mock token válido (não expira até 2099)
  const tokenValido =
    'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJleHAiOjQwOTI1NzE2MDB9.4n8a5JfK7VvEoVjWkc5jG3-ZKe3qKp1n2CjJj3TvNcw';

  // Mock token expirado (2020)
  const tokenExpirado =
    'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJleHAiOjE1Nzc4ODAwMDB9.invalid';

  // Token malformado
  const tokenMalformado = 'invalid.token.here';

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

  describe('salvarToken', () => {
    it('deve salvar token no localStorage', () => {
      const token = 'test.token.123';

      service.salvarToken(token);

      expect(localStorage.getItem('entrega_segura_dev_token')).toBe(token);
    });
  });

  describe('obterToken', () => {
    it('deve recuperar token do localStorage', () => {
      const token = 'test.token.123';
      localStorage.setItem('entrega_segura_dev_token', token);

      const result = service.obterToken();

      expect(result).toBe(token);
    });

    it('deve retornar null quando não há token', () => {
      const result = service.obterToken();

      expect(result).toBeNull();
    });
  });

  describe('removeToken', () => {
    it('deve remover token do localStorage', () => {
      localStorage.setItem('entrega_segura_dev_token', 'test-token');

      service.removerToken();

      expect(localStorage.getItem('entrega_segura_dev_token')).toBeNull();
    });
  });

  describe('verificarTokenExpirado', () => {
    it('deve retornar false para token válido', () => {
      const result = service.verificarTokenExpirado(tokenValido);

      expect(result).toBe(false);
    });

    it('deve retornar true para token expirado', () => {
      const result = service.verificarTokenExpirado(tokenExpirado);

      expect(result).toBe(true);
    });

    it('deve retornar true para token malformado', () => {
      const result = service.verificarTokenExpirado(tokenMalformado);

      expect(result).toBe(true);
    });

    it('deve retornar true para token vazio', () => {
      const result = service.verificarTokenExpirado('');

      expect(result).toBe(true);
    });

    it('deve retornar true para token que não pode ser decodificado', () => {
      const result = service.verificarTokenExpirado('invalid');

      expect(result).toBe(true);
    });
  });

  describe('métodos privados', () => {
    it('deve verificar se localStorage está disponível (caso feliz)', () => {
      // Testamos indiretamente através dos métodos públicos
      service.salvarToken('test');
      const result = service.obterToken();

      expect(result).toBe('test');
    });

    it('deve decodificar JWT corretamente', () => {
      // Testamos indiretamente através do isTokenExpired
      const result = service.verificarTokenExpirado(tokenValido);

      expect(result).toBe(false);
    });
  });

  describe('edge cases', () => {
    it('deve lidar com token JWT sem pontos', () => {
      const result = service.verificarTokenExpirado('tokenSemPontos');

      expect(result).toBe(true);
    });

    it('deve lidar com payload JWT inválido', () => {
      const invalidJWT = 'header.invalidBase64Payload.signature';
      const result = service.verificarTokenExpirado(invalidJWT);

      expect(result).toBe(true);
    });

    it('deve lidar com token que tem payload que não é JSON válido', () => {
      // Token com payload que não pode ser parsed
      const invalidJsonJWT = 'header.aW52YWxpZF9qc29u.signature'; // "invalid_json" em base64
      const result = service.verificarTokenExpirado(invalidJsonJWT);

      expect(result).toBe(true);
    });

    it('deve lidar com token sem partes suficientes', () => {
      const result = service.verificarTokenExpirado('apenas.uma.parte.demais');

      expect(result).toBe(true);
    });
  });

  describe('tratamento de erros', () => {
    it('deve lidar com localStorage indisponível no salvarToken', () => {
      const consoleSpy = jest.spyOn(console, 'warn').mockImplementation();

      // Simular localStorage indisponível temporariamente
      const originalLocalStorage = global.localStorage;
      delete (global as Record<string, unknown>)['localStorage'];

      service.salvarToken('test-token');

      // Restaurar localStorage
      global.localStorage = originalLocalStorage;

      // Verificar que o token não foi salvo
      expect(localStorage.getItem('entrega_segura_dev_token')).toBeNull();

      consoleSpy.mockRestore();
    });

    it('deve lidar com localStorage indisponível no obterToken', () => {
      // Simular localStorage indisponível
      const originalLocalStorage = global.localStorage;
      delete (global as Record<string, unknown>)['localStorage'];

      const result = service.obterToken();

      // Restaurar localStorage
      global.localStorage = originalLocalStorage;

      expect(result).toBeNull();
    });

    it('deve lidar com localStorage indisponível no removerToken', () => {
      // Simular localStorage indisponível
      const originalLocalStorage = global.localStorage;
      delete (global as Record<string, unknown>)['localStorage'];

      // Não deve lançar erro
      expect(() => service.removerToken()).not.toThrow();

      // Restaurar localStorage
      global.localStorage = originalLocalStorage;
    });
  });
});
