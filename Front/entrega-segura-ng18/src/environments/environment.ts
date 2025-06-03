import { Environment } from './environment.interface';
import { TIMEOUTS, PAGINATION, STORAGE_KEYS, LOG_LEVELS } from '../app/core/constants/app.constants';

export const environment: Environment = {
  production: false,
  version: '1.0.0-dev',
  appName: 'EntregaSegura Development',

  api: {
    baseUrl: 'https://localhost:5001',
    timeout: TIMEOUTS.API_REQUEST,
    retryAttempts: 3,
    endpoints: {
      autenticacao: '/api/usuario',
      entregas: '/api/entregas',
      condominios: '/api/condominios',
      usuarios: '/api/usuarios',
      transportadoras: '/api/transportadoras',
    },
  },

  autenticacao: {
    tokenKey: `${STORAGE_KEYS.TOKEN}_dev`,
    sessionTimeout: TIMEOUTS.SESSION,
    refreshThreshold: TIMEOUTS.REFRESH_THRESHOLD,
    enableAutoRefresh: true,
  },

  features: {
    enableAnalytics: false,
    enableDebugMode: true,
    enableMockData: false,
    enableServiceWorker: false,
    enableErrorReporting: false,
    enablePerformanceMonitoring: true,
  },

  ui: {
    theme: 'light',
    itemsPerPage: PAGINATION.DEFAULT_PAGE_SIZE,
    animationsEnabled: true,
    showBetaFeatures: true,
    defaultLanguage: 'pt-BR',
  },

  logging: {
    level: LOG_LEVELS.DEBUG,
    enableConsoleLog: true,
    enableRemoteLogging: false,
  },

  external: {
    // googleAnalyticsId: undefined, // Não rastrear em dev
    // sentryDsn: undefined, // Não reportar erros em dev
  },
};
