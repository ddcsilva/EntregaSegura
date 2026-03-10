export interface Environment {
  production: boolean;
  version: string;
  appName: string;

  api: {
    baseUrl: string;
    timeout: number;
    retryAttempts: number;
    endpoints: {
      autenticacao: string;
      entregas: string;
      condominios: string;
      usuarios: string;
      transportadoras: string;
    };
  };

  autenticacao: {
    tokenKey: string;
    sessionTimeout: number;
    refreshThreshold: number;
    enableAutoRefresh: boolean;
  };

  features: {
    enableAnalytics: boolean;
    enableDebugMode: boolean;
    enableMockData: boolean;
    enableServiceWorker: boolean;
    enableErrorReporting: boolean;
    enablePerformanceMonitoring: boolean;
  };

  ui: {
    theme: 'light' | 'dark' | 'auto';
    itemsPerPage: number;
    animationsEnabled: boolean;
    showBetaFeatures: boolean;
    defaultLanguage: string;
  };

  logging: {
    level: 'debug' | 'info' | 'warn' | 'error';
    enableConsoleLog: boolean;
    enableRemoteLogging: boolean;
    logEndpoint?: string;
  };

  external?: {
    googleAnalyticsId?: string;
    sentryDsn?: string;
    hotjarId?: string;
  };
}
