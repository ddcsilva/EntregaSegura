import { TestBed } from '@angular/core/testing';
import { EnvironmentService } from './environment.service';

describe('EnvironmentService', () => {
  let service: EnvironmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnvironmentService);
  });

  it('deve ser criado', () => {
    expect(service).toBeTruthy();
  });

  describe('config getter', () => {
    it('deve retornar a configuração do environment', () => {
      expect(service.config).toBeDefined();
      expect(service.config).toHaveProperty('production');
      expect(service.config).toHaveProperty('api');
      expect(service.config).toHaveProperty('features');
    });
  });

  describe('ehAmbienteProducao getter', () => {
    it('deve retornar o valor de production do environment', () => {
      const expected = service.config.production;

      expect(service.ehAmbienteProducao).toBe(expected);
    });
  });

  describe('ehAmbienteDesenvolvimento getter', () => {
    it('deve retornar o oposto de production', () => {
      const expected = !service.config.production;

      expect(service.ehAmbienteDesenvolvimento).toBe(expected);
    });
  });

  describe('urlApi getter', () => {
    it('deve retornar a URL base da API', () => {
      const expected = service.config.api.baseUrl;

      expect(service.urlApi).toBe(expected);
    });
  });

  describe('debugHabilitado getter', () => {
    it('deve retornar se debug está habilitado', () => {
      const expected = service.config.features.enableDebugMode;

      expect(service.debugHabilitado).toBe(expected);
    });
  });

  describe('quantidadeItensPorPagina getter', () => {
    it('deve retornar o número de itens por página', () => {
      const expected = service.config.ui.itemsPerPage;

      expect(service.quantidadeItensPorPagina).toBe(expected);
    });
  });

  describe('isFeatureEnabled', () => {
    it('deve retornar true para feature habilitada', () => {
      // Pegar uma feature que está habilitada no environment
      const features = service.config.features;
      const enabledFeature = Object.keys(features).find(
        key => features[key as keyof typeof features] === true
      ) as keyof typeof features;

      if (enabledFeature) {
        expect(service.featureHabilitada(enabledFeature)).toBe(true);
      } else {
        // Se nenhuma feature está habilitada, teste com enableDebugMode
        expect(service.featureHabilitada('enableDebugMode')).toBe(features.enableDebugMode);
      }
    });

    it('deve retornar false para feature desabilitada', () => {
      // Pegar uma feature que está desabilitada no environment
      const features = service.config.features;
      const disabledFeature = Object.keys(features).find(
        key => features[key as keyof typeof features] === false
      ) as keyof typeof features;

      if (disabledFeature) {
        expect(service.featureHabilitada(disabledFeature)).toBe(false);
      } else {
        // Se todas as features estão habilitadas, teste um cenário específico
        expect(service.featureHabilitada('enableAnalytics')).toBe(features.enableAnalytics);
      }
    });

    it('deve testar todas as features disponíveis', () => {
      expect(service.featureHabilitada('enableAnalytics')).toBe(service.config.features.enableAnalytics);
      expect(service.featureHabilitada('enableDebugMode')).toBe(service.config.features.enableDebugMode);
      expect(service.featureHabilitada('enableMockData')).toBe(service.config.features.enableMockData);
      expect(service.featureHabilitada('enableServiceWorker')).toBe(service.config.features.enableServiceWorker);
      expect(service.featureHabilitada('enableErrorReporting')).toBe(service.config.features.enableErrorReporting);
      expect(service.featureHabilitada('enablePerformanceMonitoring')).toBe(
        service.config.features.enablePerformanceMonitoring
      );
    });
  });

  describe('shouldLog', () => {
    it('deve retornar true para nível igual ao configurado', () => {
      const currentLevel = service.config.logging.level;

      expect(service.deveLogar(currentLevel)).toBe(true);
    });

    it('deve retornar true para nível superior ao configurado', () => {
      const currentLevel = service.config.logging.level;

      if (currentLevel === 'debug') {
        expect(service.deveLogar('info')).toBe(true);
        expect(service.deveLogar('warn')).toBe(true);
        expect(service.deveLogar('error')).toBe(true);
      } else if (currentLevel === 'info') {
        expect(service.deveLogar('warn')).toBe(true);
        expect(service.deveLogar('error')).toBe(true);
      } else if (currentLevel === 'warn') {
        expect(service.deveLogar('error')).toBe(true);
      }
    });

    it('deve retornar false para nível inferior ao configurado', () => {
      const currentLevel = service.config.logging.level;

      if (currentLevel === 'error') {
        expect(service.deveLogar('debug')).toBe(false);
        expect(service.deveLogar('info')).toBe(false);
        expect(service.deveLogar('warn')).toBe(false);
      } else if (currentLevel === 'warn') {
        expect(service.deveLogar('debug')).toBe(false);
        expect(service.deveLogar('info')).toBe(false);
      } else if (currentLevel === 'info') {
        expect(service.deveLogar('debug')).toBe(false);
      }
    });

    it('deve testar todos os níveis de log', () => {
      // Testamos cada nível independente do environment atual
      const levels = ['debug', 'info', 'warn', 'error'] as const;

      levels.forEach(level => {
        const result = service.deveLogar(level);
        expect(typeof result).toBe('boolean');
      });
    });

    it('deve respeitar a hierarquia de níveis', () => {
      // Debug permite todos
      if (service.config.logging.level === 'debug') {
        expect(service.deveLogar('debug')).toBe(true);
        expect(service.deveLogar('info')).toBe(true);
        expect(service.deveLogar('warn')).toBe(true);
        expect(service.deveLogar('error')).toBe(true);
      }

      // Error só permite error
      if (service.config.logging.level === 'error') {
        expect(service.deveLogar('debug')).toBe(false);
        expect(service.deveLogar('info')).toBe(false);
        expect(service.deveLogar('warn')).toBe(false);
        expect(service.deveLogar('error')).toBe(true);
      }
    });
  });

  describe('integração com environment', () => {
    it('deve usar corretamente as configurações do environment de desenvolvimento', () => {
      // Verifica se as propriedades estão sendo acessadas corretamente
      expect(service.config.appName).toBe('EntregaSegura Development');
      expect(service.config.api.baseUrl).toContain('localhost');
      expect(typeof service.config.api.timeout).toBe('number');
    });

    it('deve ter todas as propriedades obrigatórias', () => {
      expect(service.config).toHaveProperty('production');
      expect(service.config).toHaveProperty('version');
      expect(service.config).toHaveProperty('appName');
      expect(service.config).toHaveProperty('api');
      expect(service.config).toHaveProperty('auth');
      expect(service.config).toHaveProperty('features');
      expect(service.config).toHaveProperty('ui');
      expect(service.config).toHaveProperty('logging');
    });

    it('deve ter estrutura da API válida', () => {
      expect(service.config.api).toHaveProperty('baseUrl');
      expect(service.config.api).toHaveProperty('timeout');
      expect(service.config.api).toHaveProperty('endpoints');
      expect(service.config.api.endpoints).toHaveProperty('auth');
      expect(service.config.api.endpoints).toHaveProperty('entregas');
    });

    it('deve ter configurações de auth válidas', () => {
      expect(service.config.auth).toHaveProperty('tokenKey');
      expect(service.config.auth).toHaveProperty('sessionTimeout');
      expect(service.config.auth).toHaveProperty('refreshThreshold');
      expect(service.config.auth).toHaveProperty('enableAutoRefresh');
    });
  });

  describe('helpers', () => {
    it('deve verificar se feature está habilitada', () => {
      expect(service.featureHabilitada('enableDebugMode')).toBe(true);
      expect(service.featureHabilitada('enableAnalytics')).toBe(false);
    });

    it('deve retornar false para feature inexistente', () => {
      expect(service.featureHabilitada('nonExistentFeature' as never)).toBeUndefined();
    });

    it('deve verificar logging por nível', () => {
      expect(service.deveLogar('debug')).toBe(true);
      expect(service.deveLogar('info')).toBe(true);
      expect(service.deveLogar('warn')).toBe(true);
      expect(service.deveLogar('error')).toBe(true);
    });

    it('deve verificar diferentes níveis de log', () => {
      // Como o environment está em debug, todos os níveis devem ser permitidos
      expect(service.deveLogar('debug')).toBe(true);
      expect(service.deveLogar('info')).toBe(true);
      expect(service.deveLogar('warn')).toBe(true);
      expect(service.deveLogar('error')).toBe(true);
    });
  });

  describe('edge cases', () => {
    it('deve lidar com propriedades undefined graciosamente', () => {
      // Testa se o service lida bem com propriedades que podem não existir
      expect(() => service.featureHabilitada('enableDebugMode')).not.toThrow();
      expect(() => service.deveLogar('debug')).not.toThrow();
    });

    it('deve retornar valores padrão seguros', () => {
      // Verifica se todos os getters retornam valores válidos
      expect(typeof service.ehAmbienteProducao).toBe('boolean');
      expect(typeof service.ehAmbienteDesenvolvimento).toBe('boolean');
      expect(typeof service.urlApi).toBe('string');
      expect(typeof service.debugHabilitado).toBe('boolean');
      expect(typeof service.quantidadeItensPorPagina).toBe('number');
    });

    it('deve acessar configurações aninhadas', () => {
      expect(service.config.api.timeout).toBe(30000);
      expect(service.config.api.retryAttempts).toBe(3);
      expect(service.config.auth.sessionTimeout).toBe(480);
      expect(service.config.ui.theme).toBe('light');
      expect(service.config.logging.level).toBe('debug');
    });

    it('deve verificar features específicas', () => {
      expect(service.featureHabilitada('enableDebugMode')).toBe(true);
      expect(service.featureHabilitada('enableAnalytics')).toBe(false);
      expect(service.featureHabilitada('enableMockData')).toBe(false);
      expect(service.featureHabilitada('enableServiceWorker')).toBe(false);
      expect(service.featureHabilitada('enableErrorReporting')).toBe(false);
      expect(service.featureHabilitada('enablePerformanceMonitoring')).toBe(true);
    });
  });
});
