import { Environment } from './environment.interface';

export const environment: Environment = {
  production: false,
  version: '1.0.0-dev',
  appName: 'EntregaSegura Development',

  api: {
    baseUrl: 'https://localhost:5001',
    timeout: 30000,
    retryAttempts: 3,
    endpoints: {
      auth: '/api/usuario',
      entregas: '/api/entregas',
      condominios: '/api/condominios',
      usuarios: '/api/usuarios',
      transportadoras: '/api/transportadoras',
    },
  },

  auth: {
    tokenKey: 'entrega_segura_dev_token',
    sessionTimeout: 480,
    refreshThreshold: 30,
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
    itemsPerPage: 10,
    animationsEnabled: true,
    showBetaFeatures: true,
    defaultLanguage: 'pt-BR',
  },

  logging: {
    level: 'debug',
    enableConsoleLog: true,
    enableRemoteLogging: false,
  },

  external: {
    // googleAnalyticsId: undefined, // Não rastrear em dev
    // sentryDsn: undefined, // Não reportar erros em dev
  },
};
