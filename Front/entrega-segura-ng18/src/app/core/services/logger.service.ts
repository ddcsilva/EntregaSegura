import { Injectable, isDevMode } from '@angular/core';
import { environment } from '@environments';
import { LOG_LEVELS, LogLevel } from '../constants/app.constants';

/**
 * Interface para configuração de log
 */
interface LogEntry {
  level: LogLevel;
  message: string;
  timestamp: Date;
  data?: any;
  error?: Error;
  context?: string;
}

/**
 * Service centralizado para logging
 * Controla logs baseado no ambiente e permite integração com serviços externos
 */
@Injectable({
  providedIn: 'root',
})
export class LoggerService {
  private readonly isDevelopment = isDevMode();
  private readonly logLevel = environment.logging?.level || LOG_LEVELS.INFO;
  private readonly enableConsole = environment.logging?.enableConsoleLog ?? this.isDevelopment;
  private readonly enableRemote = environment.logging?.enableRemoteLogging ?? false;

  /**
   * Log de erro crítico
   * Sempre enviado para logging remoto em produção
   */
  error(message: string, error?: Error | any, context?: string): void {
    const logEntry: LogEntry = {
      level: LOG_LEVELS.ERROR,
      message,
      timestamp: new Date(),
      error: error instanceof Error ? error : new Error(String(error)),
      context,
      data: error && !(error instanceof Error) ? error : undefined,
    };

    this.processLog(logEntry);
  }

  /**
   * Log de warning
   * Usado para situações que não quebram a aplicação mas precisam atenção
   */
  warn(message: string, data?: any, context?: string): void {
    const logEntry: LogEntry = {
      level: LOG_LEVELS.WARN,
      message,
      timestamp: new Date(),
      data,
      context,
    };

    this.processLog(logEntry);
  }

  /**
   * Log informativo
   * Para informações importantes sobre o fluxo da aplicação
   */
  info(message: string, data?: any, context?: string): void {
    const logEntry: LogEntry = {
      level: LOG_LEVELS.INFO,
      message,
      timestamp: new Date(),
      data,
      context,
    };

    this.processLog(logEntry);
  }

  /**
   * Log de debug
   * Apenas em desenvolvimento, para debugging detalhado
   */
  debug(message: string, data?: any, context?: string): void {
    // Debug só em desenvolvimento
    if (!this.isDevelopment) return;

    const logEntry: LogEntry = {
      level: LOG_LEVELS.DEBUG,
      message,
      timestamp: new Date(),
      data,
      context,
    };

    this.processLog(logEntry);
  }

  /**
   * Log de timing para performance
   */
  time(label: string): void {
    if (this.enableConsole && this.shouldLog(LOG_LEVELS.DEBUG)) {
      console.time(label);
    }
  }

  timeEnd(label: string): void {
    if (this.enableConsole && this.shouldLog(LOG_LEVELS.DEBUG)) {
      console.timeEnd(label);
    }
  }

  /**
   * Processa o log de acordo com as configurações
   */
  private processLog(logEntry: LogEntry): void {
    if (!this.shouldLog(logEntry.level)) return;

    // Log no console (apenas em dev ou se habilitado)
    if (this.enableConsole) {
      this.logToConsole(logEntry);
    }

    // Log remoto (em produção ou se habilitado)
    if (this.enableRemote && this.shouldSendToRemote(logEntry)) {
      this.logToRemote(logEntry);
    }
  }

  /**
   * Verifica se deve fazer log baseado no nível configurado
   */
  private shouldLog(level: LogLevel): boolean {
    const levels = [LOG_LEVELS.ERROR, LOG_LEVELS.WARN, LOG_LEVELS.INFO, LOG_LEVELS.DEBUG];
    const currentLevelIndex = levels.indexOf(this.logLevel);
    const logLevelIndex = levels.indexOf(level);

    return logLevelIndex <= currentLevelIndex;
  }

  /**
   * Determina se deve enviar para logging remoto
   */
  private shouldSendToRemote(logEntry: LogEntry): boolean {
    // Sempre enviar erros
    if (logEntry.level === LOG_LEVELS.ERROR) return true;

    // Em produção, enviar warnings também
    if (!this.isDevelopment && logEntry.level === LOG_LEVELS.WARN) return true;

    return false;
  }

  /**
   * Faz log no console com formatação adequada
   */
  private logToConsole(logEntry: LogEntry): void {
    const { level, message, timestamp, data, error, context } = logEntry;
    const prefix = `[${timestamp.toISOString()}]${context ? ` [${context}]` : ''}`;

    switch (level) {
      case LOG_LEVELS.ERROR:
        console.error(`${prefix} ERROR: ${message}`, error || data);
        break;
      case LOG_LEVELS.WARN:
        console.warn(`${prefix} WARN: ${message}`, data);
        break;
      case LOG_LEVELS.INFO:
        console.info(`${prefix} INFO: ${message}`, data);
        break;
      case LOG_LEVELS.DEBUG:
        console.log(`${prefix} DEBUG: ${message}`, data);
        break;
    }
  }

  /**
   * Envia log para serviço remoto
   * TODO: Implementar integração com Sentry, LogRocket, etc.
   */
  private logToRemote(logEntry: LogEntry): void {
    // Implementar integração com serviço de logging remoto
    // Exemplo: Sentry, LogRocket, CloudWatch, etc.

    // Por enquanto, apenas preparamos os dados
    const remoteLog = {
      level: logEntry.level,
      message: logEntry.message,
      timestamp: logEntry.timestamp.toISOString(),
      url: window.location.href,
      userAgent: navigator.userAgent,
      userId: this.getCurrentUserId(),
      context: logEntry.context,
      data: logEntry.data,
      error: logEntry.error
        ? {
            name: logEntry.error.name,
            message: logEntry.error.message,
            stack: logEntry.error.stack,
          }
        : undefined,
    };

    // TODO: Enviar para serviço remoto
    console.log('Log para serviço remoto:', remoteLog);
  }

  /**
   * Obtém ID do usuário atual para contexto de logs
   */
  private getCurrentUserId(): string | null {
    // TODO: Integrar com AuthService quando refatorarmos
    try {
      const token = localStorage.getItem('entrega_segura_token');
      if (token) {
        const payload = JSON.parse(atob(token.split('.')[1]));
        return payload.Id || null;
      }
    } catch {
      // Silencioso
    }
    return null;
  }
}
