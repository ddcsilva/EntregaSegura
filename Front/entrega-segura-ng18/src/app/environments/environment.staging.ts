import { Environment } from './environment.interface';

export const environment: Environment = {
  production: false,
  version: '1.0.0-staging',
  appName: 'EntregaSegura Homologação',

  api: {
    baseUrl: 'https://api-staging.entregasegura.com.br',
    timeout: 15000,
    retryAttempts: 2,
    endpoints: {
      auth: '/api/usuario',
      entregas: '/api/entregas',
      condominios: '/api/condominios',
      usuarios: '/api/usuarios',
      transportadoras: '/api/transportadoras',
    },
  },

  auth: {
    tokenKey: 'entrega_segura_staging_token',
    sessionTimeout: 240,
    refreshThreshold: 15,
    enableAutoRefresh: true,
  },

  features: {
    enableAnalytics: false,
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
    level: 'info',
    enableConsoleLog: false,
    enableRemoteLogging: true,
    logEndpoint: 'https://logs-staging.entregasegura.com.br/api/logs',
  },

  external: {
    googleAnalyticsId: 'GA-STAGING-ID',
    sentryDsn: 'https://staging-dsn@sentry.io/project-id',
  },
};
