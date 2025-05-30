import { Environment } from './environment.interface';

export const environment: Environment = {
  // 🏗️ Build Configuration
  production: false,
  version: '1.0.0-dev',
  appName: 'EntregaSegura Development',

  // 🌐 API Configuration - Local Development
  api: {
    baseUrl: 'https://localhost:5001',
    timeout: 30000, // 30 segundos para debug
    retryAttempts: 3,
    endpoints: {
      auth: '/api/usuario',
      entregas: '/api/entregas',
      condominios: '/api/condominios',
      usuarios: '/api/usuarios',
      transportadoras: '/api/transportadoras',
    },
  },

  // 🔐 Authentication - Development Settings
  auth: {
    tokenKey: 'entrega_segura_dev_token',
    sessionTimeout: 480, // 8 horas para desenvolvimento
    refreshThreshold: 30, // 30 minutos antes de expirar
    enableAutoRefresh: true,
  },

  // 📊 Features - Tudo habilitado para desenvolvimento
  features: {
    enableAnalytics: false, // Não poluir analytics em dev
    enableDebugMode: true,
    enableMockData: false, // Usar API real para desenvolvimento
    enableServiceWorker: false, // PWA desabilitado em dev
    enableErrorReporting: false, // Não enviar erros de dev
    enablePerformanceMonitoring: true, // Monitorar performance local
  },

  // 🎨 UI/UX - Configurações de desenvolvimento
  ui: {
    theme: 'light',
    itemsPerPage: 10, // Menor para debug
    animationsEnabled: true,
    showBetaFeatures: true, // Mostrar features experimentais
    defaultLanguage: 'pt-BR',
  },

  // 📝 Logging - Verbose para desenvolvimento
  logging: {
    level: 'debug',
    enableConsoleLog: true,
    enableRemoteLogging: false, // Não enviar logs remotos em dev
  },

  // 🔗 External Services - Desabilitados em desenvolvimento
  external: {
    // googleAnalyticsId: undefined, // Não rastrear em dev
    // sentryDsn: undefined, // Não reportar erros em dev
  },
};
