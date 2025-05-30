import { Environment } from './environment.interface';

export const environment: Environment = {
  // 🏗️ Build Configuration
  production: false, // Ainda é ambiente de teste
  version: '1.0.0-staging',
  appName: 'EntregaSegura Homologação',

  // 🌐 API Configuration - Servidor de homologação
  api: {
    baseUrl: 'https://api-staging.entregasegura.com.br',
    timeout: 15000, // Timeout mais restrito
    retryAttempts: 2,
    endpoints: {
      auth: '/api/usuario',
      entregas: '/api/entregas',
      condominios: '/api/condominios',
      usuarios: '/api/usuarios',
      transportadoras: '/api/transportadoras',
    },
  },

  // 🔐 Authentication - Configurações realistas
  auth: {
    tokenKey: 'entrega_segura_staging_token',
    sessionTimeout: 240, // 4 horas - mais realista
    refreshThreshold: 15, // 15 minutos antes de expirar
    enableAutoRefresh: true,
  },

  // 📊 Features - Configuração próxima à produção
  features: {
    enableAnalytics: false, // Analytics de teste separado
    enableDebugMode: false, // Debug desabilitado
    enableMockData: false, // API real sempre
    enableServiceWorker: true, // Testar PWA
    enableErrorReporting: true, // Reportar erros para análise
    enablePerformanceMonitoring: true,
  },

  // 🎨 UI/UX - Configurações de produção
  ui: {
    theme: 'light',
    itemsPerPage: 20, // Mesmo valor da produção
    animationsEnabled: true,
    showBetaFeatures: false, // Ocultar features experimentais
    defaultLanguage: 'pt-BR',
  },

  // 📝 Logging - Menos verbose
  logging: {
    level: 'info',
    enableConsoleLog: false, // Console limpo
    enableRemoteLogging: true,
    logEndpoint: 'https://logs-staging.entregasegura.com.br/api/logs',
  },

  // 🔗 External Services - Ambientes de teste
  external: {
    googleAnalyticsId: 'GA-STAGING-ID', // Analytics de teste
    sentryDsn: 'https://staging-dsn@sentry.io/project-id',
  },
};
