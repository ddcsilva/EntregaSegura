export * from './environment.interface';
export { environment } from './environment';

export function ValidarAmbiente(env: unknown): boolean {
  if (!env || typeof env !== 'object') {
    return false;
  }

  const required = ['production', 'version', 'api.baseUrl', 'auth.tokenKey'];

  return required.every(path => {
    const value = path
      .split('.')
      .reduce(
        (obj: unknown, key: string): unknown =>
          obj && typeof obj === 'object' ? (obj as Record<string, unknown>)[key] : undefined,
        env
      );
    return value !== undefined && value !== null && value !== '';
  });
}

export function verificarSituacaoAmbiente(): void {
  import('./environment')
    .then(({ environment }) => {
      if (!ValidarAmbiente(environment)) {
        console.error('Configuração do ambiente inválida!');
        console.table(environment);
        throw new Error('Configuração do ambiente está incompleta ou inválida');
      }

      if (!environment.production) {
        console.log('Executando em ambiente de desenvolvimento');
        console.table({
          Ambiente: environment.production ? 'Produção' : 'Desenvolvimento',
          Versão: environment.version,
          API: environment.api.baseUrl,
          Debug: environment.features.enableDebugMode,
          'Mock Data': environment.features.enableMockData,
        });
      }
    })
    .catch(error => {
      console.warn('Verificação do ambiente falhou:', error.message);
    });
}
