import { Environment } from './environment.interface';

export const environment: Environment = {
  production: true,
  version: '1.0.0',
  appName: 'EntregaSegura',

  api: {
    baseUrl: 'https://api.entregasegura.com.br',
    timeout: 10000,
    retryAttempts: 2,
    endpoints: {
      autenticacao: '/api/usuario',
      entregas: '/api/entregas',
      condominios: '/api/condominios',
      usuarios: '/api/usuarios',
      transportadoras: '/api/transportadoras',
    },
  },

  autenticacao: {
    tokenKey: 'entrega_segura_token',
    sessionTimeout: 120,
    refreshThreshold: 10,
    enableAutoRefresh: true,
  },

  features: {
    enableAnalytics: true,
    enableDebugMode: false,
    enableMockData: false,
    enableServiceWorker: true,
    enableErrorReporting: true,
    enablePerformanceMonitoring: true,
  },

  ui: {
    theme: 'light',
    itemsPerPage: 20,
    animationsEnabled: true,
    showBetaFeatures: false,
    defaultLanguage: 'pt-BR',
  },

  logging: {
    level: 'error',
    enableConsoleLog: false,
    enableRemoteLogging: true,
    logEndpoint: 'https://logs.entregasegura.com.br/api/logs',
  },

  external: {
    googleAnalyticsId: 'G-XXXXXXXXXX',
    sentryDsn: 'https://production-dsn@sentry.io/project-id',
  },
};
