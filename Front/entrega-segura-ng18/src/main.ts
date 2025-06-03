import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { verificarSituacaoAmbiente } from '@environments';

// Verificar ambiente antes de iniciar a aplicação
verificarSituacaoAmbiente();

// Bootstrap da aplicação com tratamento de erro mais robusto
bootstrapApplication(AppComponent, appConfig).catch(err => {
  // O GlobalErrorHandler já está configurado, mas para erros de bootstrap
  // precisamos de fallback manual
  console.error('Erro crítico ao inicializar aplicação:', err);

  // Tentar mostrar uma mensagem de erro ao usuário
  const errorMessage = document.createElement('div');
  errorMessage.innerHTML = `
    <div style="
      position: fixed;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      background: #f8d7da;
      color: #721c24;
      padding: 20px;
      border-radius: 8px;
      border: 1px solid #f5c6cb;
      max-width: 400px;
      text-align: center;
      z-index: 9999;
      font-family: Arial, sans-serif;
    ">
      <h3 style="margin: 0 0 10px 0;">Erro ao carregar aplicação</h3>
      <p style="margin: 0;">A aplicação não pôde ser iniciada. Recarregue a página ou entre em contato com o suporte.</p>
      <button onclick="window.location.reload()" style="
        margin-top: 15px;
        padding: 8px 16px;
        background: #721c24;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
      ">Recarregar Página</button>
    </div>
  `;

  document.body.appendChild(errorMessage);
});
