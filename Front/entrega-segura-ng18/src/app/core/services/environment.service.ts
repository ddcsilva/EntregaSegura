import { Injectable } from '@angular/core';
import { environment, Environment } from '@environments';

@Injectable({
  providedIn: 'root',
})
export class EnvironmentService {
  public readonly config: Environment = environment;

  // 🔍 Getters para acesso tipado
  public get isProduction(): boolean {
    return this.config.production;
  }

  public get isDevelopment(): boolean {
    return !this.config.production;
  }

  public get apiBaseUrl(): string {
    return this.config.api.baseUrl;
  }

  public get isDebugEnabled(): boolean {
    return this.config.features.enableDebugMode;
  }

  public get itemsPerPage(): number {
    return this.config.ui.itemsPerPage;
  }

  // 🚨 Helper para features flags
  public isFeatureEnabled(feature: keyof Environment['features']): boolean {
    return this.config.features[feature];
  }

  // 📊 Helper para logging
  public shouldLog(level: 'debug' | 'info' | 'warn' | 'error'): boolean {
    const levels = ['debug', 'info', 'warn', 'error'];
    const currentLevelIndex = levels.indexOf(this.config.logging.level);
    const requestedLevelIndex = levels.indexOf(level);

    return requestedLevelIndex >= currentLevelIndex;
  }
}
