// 📄 src/environments/environment.interface.ts
export interface Environment {
  // 🏗️ Build & Runtime Configuration
  production: boolean;
  version: string;
  appName: string;

  // 🌐 API Configuration
  api: {
    baseUrl: string;
    timeout: number;
    retryAttempts: number;
    endpoints: {
      auth: string;
      entregas: string;
      condominios: string;
      usuarios: string;
      transportadoras: string;
    };
  };

  // 🔐 Authentication Configuration
  auth: {
    tokenKey: string;
    sessionTimeout: number; // em minutos
    refreshThreshold: number; // minutos antes da expiração
    enableAutoRefresh: boolean;
  };

  // 📊 Features & Capabilities
  features: {
    enableAnalytics: boolean;
    enableDebugMode: boolean;
    enableMockData: boolean;
    enableServiceWorker: boolean;
    enableErrorReporting: boolean;
    enablePerformanceMonitoring: boolean;
  };

  // 🎨 UI/UX Configuration
  ui: {
    theme: 'light' | 'dark' | 'auto';
    itemsPerPage: number;
    animationsEnabled: boolean;
    showBetaFeatures: boolean;
    defaultLanguage: string;
  };

  // 📝 Logging Configuration
  logging: {
    level: 'debug' | 'info' | 'warn' | 'error';
    enableConsoleLog: boolean;
    enableRemoteLogging: boolean;
    logEndpoint?: string;
  };

  // 🔗 External Services (quando necessário)
  external?: {
    googleAnalyticsId?: string;
    sentryDsn?: string;
    hotjarId?: string;
  };
}
