import { Environment } from './environment.interface';

export const environment: Environment = {
  // 🏗️ Build Configuration
  production: true,
  version: '1.0.0',
  appName: 'EntregaSegura',

  // 🌐 API Configuration - Produção
  api: {
    baseUrl: 'https://api.entregasegura.com.br',
    timeout: 10000, // Timeout otimizado
    retryAttempts: 2,
    endpoints: {
      auth: '/api/usuario',
      entregas: '/api/entregas',
      condominios: '/api/condominios',
      usuarios: '/api/usuarios',
      transportadoras: '/api/transportadoras',
    },
  },

  // 🔐 Authentication - Configurações seguras
  auth: {
    tokenKey: 'entrega_segura_token',
    sessionTimeout: 120, // 2 horas - segurança
    refreshThreshold: 10, // 10 minutos antes de expirar
    enableAutoRefresh: true,
  },

  // 📊 Features - Apenas essenciais habilitadas
  features: {
    enableAnalytics: true, // Analytics de produção
    enableDebugMode: false, // Debug desabilitado
    enableMockData: false, // Nunca usar mocks em produção
    enableServiceWorker: true, // PWA habilitado
    enableErrorReporting: true, // Reportar erros críticos
    enablePerformanceMonitoring: true,
  },

  // 🎨 UI/UX - Otimizado para performance
  ui: {
    theme: 'light',
    itemsPerPage: 20,
    animationsEnabled: true,
    showBetaFeatures: false, // Nunca mostrar features beta
    defaultLanguage: 'pt-BR',
  },

  // 📝 Logging - Apenas erros críticos
  logging: {
    level: 'error',
    enableConsoleLog: false, // Console limpo em produção
    enableRemoteLogging: true,
    logEndpoint: 'https://logs.entregasegura.com.br/api/logs',
  },

  // 🔗 External Services - Produção
  external: {
    googleAnalyticsId: 'G-XXXXXXXXXX', // Analytics real
    sentryDsn: 'https://production-dsn@sentry.io/project-id',
  },
};
