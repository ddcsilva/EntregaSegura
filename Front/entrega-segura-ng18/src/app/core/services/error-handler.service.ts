import { ErrorHandler, Injectable, NgZone, inject } from '@angular/core';
import { Router } from '@angular/router';
import { LoggerService } from './logger.service';

/**
 * Interface para erros da aplicação
 */
export interface AppError {
  message: string;
  userMessage: string;
  code?: string;
  severity: 'low' | 'medium' | 'high' | 'critical';
  context?: string;
  recoverable: boolean;
}

/**
 * Service para tratamento global de erros
 * Integra com o Logger Service e fornece feedback ao usuário
 */
@Injectable({
  providedIn: 'root',
})
export class GlobalErrorHandler implements ErrorHandler {
  private readonly logger = inject(LoggerService);
  private readonly ngZone = inject(NgZone);
  private readonly router = inject(Router);

  handleError(error: any): void {
    this.ngZone.run(() => {
      const appError = this.parseError(error);

      // Log do erro
      this.logger.error(appError.message, error, appError.context || 'GlobalErrorHandler');

      // Ações baseadas na severidade
      this.handleBySeverity(appError, error);

      // Notificar usuário se necessário
      if (appError.userMessage) {
        this.notifyUser(appError);
      }
    });
  }

  /**
   * Converte erro genérico em AppError estruturado
   */
  private parseError(error: any): AppError {
    // Erro HTTP
    if (error?.status) {
      return this.parseHttpError(error);
    }

    // Erro de chunk loading (lazy loading)
    if (error?.message?.includes('Loading chunk')) {
      return {
        message: `Erro ao carregar módulo: ${error.message}`,
        userMessage: 'Erro ao carregar página. Recarregue a página.',
        code: 'CHUNK_LOAD_ERROR',
        severity: 'medium',
        context: 'LazyLoading',
        recoverable: true,
      };
    }

    // Erro de script
    if (error?.message?.includes('Script error')) {
      return {
        message: 'Erro de script externo',
        userMessage: 'Erro interno. Nossa equipe foi notificada.',
        code: 'SCRIPT_ERROR',
        severity: 'medium',
        context: 'ExternalScript',
        recoverable: true,
      };
    }

    // Erro Angular
    if (error?.ngOriginalError) {
      return this.parseError(error.ngOriginalError);
    }

    // Erro genérico
    return {
      message: error?.message || 'Erro desconhecido',
      userMessage: 'Ocorreu um erro inesperado. Nossa equipe foi notificada.',
      code: 'UNKNOWN_ERROR',
      severity: 'high',
      context: 'Application',
      recoverable: true,
    };
  }

  /**
   * Trata erros HTTP específicos
   */
  private parseHttpError(error: any): AppError {
    const status = error.status;
    const url = error.url || 'URL desconhecida';

    switch (status) {
      case 0:
        return {
          message: `Erro de conectividade - URL: ${url}`,
          userMessage: 'Problema de conexão. Verifique sua internet.',
          code: 'NETWORK_ERROR',
          severity: 'high',
          context: 'HTTP',
          recoverable: true,
        };

      case 401:
        return {
          message: `Erro de autenticação - URL: ${url}`,
          userMessage: 'Sessão expirada. Você será redirecionado.',
          code: 'UNAUTHORIZED',
          severity: 'medium',
          context: 'Authentication',
          recoverable: true,
        };

      case 403:
        return {
          message: `Acesso negado - URL: ${url}`,
          userMessage: 'Você não tem permissão para esta ação.',
          code: 'FORBIDDEN',
          severity: 'medium',
          context: 'Authorization',
          recoverable: false,
        };

      case 404:
        return {
          message: `Recurso não encontrado - URL: ${url}`,
          userMessage: 'Recurso não encontrado.',
          code: 'NOT_FOUND',
          severity: 'low',
          context: 'HTTP',
          recoverable: false,
        };

      case 500:
        return {
          message: `Erro interno do servidor - URL: ${url}`,
          userMessage: 'Erro no servidor. Nossa equipe foi notificada.',
          code: 'INTERNAL_SERVER_ERROR',
          severity: 'critical',
          context: 'Server',
          recoverable: true,
        };

      default:
        return {
          message: `Erro HTTP ${status} - URL: ${url}`,
          userMessage: 'Erro na comunicação com o servidor.',
          code: `HTTP_${status}`,
          severity: 'medium',
          context: 'HTTP',
          recoverable: true,
        };
    }
  }

  /**
   * Trata erro baseado na severidade
   */
  private handleBySeverity(appError: AppError, originalError: any): void {
    switch (appError.severity) {
      case 'critical':
        this.handleCriticalError(appError, originalError);
        break;

      case 'high':
        this.handleHighSeverityError(appError, originalError);
        break;

      case 'medium':
        this.handleMediumSeverityError(appError, originalError);
        break;

      case 'low':
        // Apenas log, sem ação adicional
        break;
    }
  }

  /**
   * Trata erros críticos
   */
  private handleCriticalError(appError: AppError, originalError: any): void {
    // Em erros críticos, pode ser necessário voltar ao dashboard
    // ou fazer logout dependendo do contexto

    if (appError.code === 'INTERNAL_SERVER_ERROR') {
      // Aguardar um momento e tentar recarregar
      setTimeout(() => {
        if (appError.recoverable) {
          this.router.navigate(['/dashboard']);
        }
      }, 3000);
    }
  }

  /**
   * Trata erros de alta severidade
   */
  private handleHighSeverityError(appError: AppError, originalError: any): void {
    if (appError.code === 'NETWORK_ERROR') {
      // Implementar retry automático ou offline mode
      this.logger.info('Tentativa de reconexão pode ser implementada aqui');
    }
  }

  /**
   * Trata erros de média severidade
   */
  private handleMediumSeverityError(appError: AppError, originalError: any): void {
    if (appError.code === 'UNAUTHORIZED') {
      // Redirecionar para login após um delay
      setTimeout(() => {
        this.router.navigate(['/autenticacao/login']);
      }, 2000);
    }

    if (appError.code === 'CHUNK_LOAD_ERROR') {
      // Sugerir reload da página
      this.logger.info('Chunk load error - considerar reload automático');
    }
  }

  /**
   * Notifica o usuário sobre o erro
   * TODO: Integrar com serviço de notificações/toast
   */
  private notifyUser(appError: AppError): void {
    // Por enquanto, apenas log
    // TODO: Implementar toast/notification service
    this.logger.info(
      'Notificação para usuário',
      {
        message: appError.userMessage,
        severity: appError.severity,
        code: appError.code,
      },
      'UserNotification'
    );

    // Exemplo de implementação futura:
    // this.notificationService.show({
    //   message: appError.userMessage,
    //   type: this.mapSeverityToToastType(appError.severity),
    //   duration: this.getDurationBySeverity(appError.severity)
    // });
  }

  /**
   * Mapeia severidade para tipo de toast
   */
  private mapSeverityToToastType(severity: AppError['severity']): string {
    const typeMap = {
      low: 'info',
      medium: 'warning',
      high: 'error',
      critical: 'error',
    };

    return typeMap[severity];
  }

  /**
   * Define duração do toast baseado na severidade
   */
  private getDurationBySeverity(severity: AppError['severity']): number {
    const durationMap = {
      low: 3000,
      medium: 5000,
      high: 7000,
      critical: 10000,
    };

    return durationMap[severity];
  }
}
