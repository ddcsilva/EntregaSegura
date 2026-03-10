import { isDevMode } from '@angular/core';
import { environment as devEnvironment } from './environment';
import { environment as stagingEnvironment } from './environment.staging';
import { environment as prodEnvironment } from './environment.prod';
import { Environment } from './environment.interface';

// Export da interface para uso externo
export * from './environment.interface';

/**
 * Determina qual environment usar baseado no modo de build
 */
function determinarEnvironment(): Environment {
  // Em modo produção, usar ambiente de produção
  if (!isDevMode()) {
    return prodEnvironment;
  }

  // Verificar se há configuração específica na URL (útil para staging)
  const url = window.location.hostname;

  if (url.includes('staging') || url.includes('homolog')) {
    return stagingEnvironment;
  }

  // Padrão: desenvolvimento
  return devEnvironment;
}

/**
 * Environment ativo baseado no contexto
 */
export const environment = determinarEnvironment();

/**
 * Verifica se a configuração do ambiente está válida
 */
export function verificarSituacaoAmbiente(): void {
  try {
    // Verificações básicas
    if (!environment.api?.baseUrl) {
      throw new Error('API baseUrl não configurada');
    }

    if (!environment.autenticacao?.tokenKey) {
      throw new Error('Token key não configurada');
    }

    // Log do ambiente ativo (apenas console para evitar dependência circular)
    if (isDevMode()) {
      console.log(`🚀 EntregaSegura iniciado em modo: ${environment.production ? 'PRODUÇÃO' : 'DESENVOLVIMENTO'}`);
      console.log(`📡 API: ${environment.api.baseUrl}`);
      console.log(`🔧 Versão: ${environment.version}`);
      console.log(`📊 Debug: ${environment.features?.enableDebugMode ? 'Ativado' : 'Desativado'}`);
    }

    // Aviso para ambiente de produção
    if (environment.production && environment.features?.enableDebugMode) {
      console.warn('⚠️ Debug mode ativado em produção - verificar configuração');
    }
  } catch (error: any) {
    console.error('❌ Configuração do ambiente inválida!', error.message);

    // Em produção, não expor detalhes do erro
    if (environment.production) {
      throw new Error('Erro na configuração da aplicação');
    } else {
      throw error;
    }
  }
}

/**
 * Utilitários para verificação de features
 */
export function isFeatureEnabled(feature: keyof NonNullable<Environment['features']>): boolean {
  return environment.features?.[feature] ?? false;
}

export function getApiEndpoint(endpoint: keyof NonNullable<Environment['api']['endpoints']>): string {
  const baseUrl = environment.api.baseUrl;
  const endpointPath = environment.api.endpoints?.[endpoint];

  if (!endpointPath) {
    throw new Error(`Endpoint '${endpoint}' não configurado`);
  }

  return `${baseUrl}${endpointPath}`;
}
