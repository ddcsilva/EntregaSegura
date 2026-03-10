import { Injectable } from '@angular/core';
import { environment, Environment } from '@environments';

@Injectable({
  providedIn: 'root',
})
export class EnvironmentService {
  public readonly config: Environment = environment;

  public get ehAmbienteProducao(): boolean {
    return this.config.production;
  }

  public get ehAmbienteDesenvolvimento(): boolean {
    return !this.config.production;
  }

  public get urlApi(): string {
    return this.config.api.baseUrl;
  }

  public get debugHabilitado(): boolean {
    return this.config.features.enableDebugMode;
  }

  public get quantidadeItensPorPagina(): number {
    return this.config.ui.itemsPerPage;
  }

  public featureHabilitada(feature: keyof Environment['features']): boolean {
    return this.config.features[feature];
  }

  public deveLogar(nivel: 'debug' | 'info' | 'warn' | 'error'): boolean {
    const levels = ['debug', 'info', 'warn', 'error'];
    const currentLevelIndex = levels.indexOf(this.config.logging.level);
    const requestedLevelIndex = levels.indexOf(nivel);

    return requestedLevelIndex >= currentLevelIndex;
  }
}
