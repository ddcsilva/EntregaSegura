export * from './environment.interface';
export { environment } from './environment';

// 🔍 Helper para validar environment
export function validateEnvironment(env: any): boolean {
  const required = ['production', 'version', 'api.baseUrl', 'auth.tokenKey'];

  return required.every(path => {
    const value = path.split('.').reduce((obj, key) => obj?.[key], env);
    return value !== undefined && value !== null && value !== '';
  });
}

// 🚨 Verificação runtime de environment
export function checkEnvironmentHealth(): void {
  // Import direto aqui para evitar problemas
  import('./environment')
    .then(({ environment }) => {
      if (!validateEnvironment(environment)) {
        console.error('❌ Environment configuration inválida!');
        console.table(environment);
        throw new Error('Environment configuration está incompleta ou inválida');
      }

      if (!environment.production) {
        console.log('🔧 Rodando em modo desenvolvimento');
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
      console.warn('Environment health check failed:', error.message);
    });
}
