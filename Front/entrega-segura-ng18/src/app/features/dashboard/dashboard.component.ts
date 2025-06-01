import { Component, computed, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AutenticacaoService } from '@core/services';
import { Papel } from '@core/models';

interface Widget {
  id: string;
  titulo: string;
  valor: number | string;
  subtitulo?: string;
  icone: string;
  cor: 'blue' | 'green' | 'yellow' | 'red' | 'purple' | 'indigo';
  tendencia?: {
    valor: number;
    tipo: 'aumento' | 'diminuicao';
  };
  papeisPermitidos?: Papel[];
}

@Component({
  selector: 'es-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  private readonly autenticacaoService = inject(AutenticacaoService);

  private readonly widgets = signal<Widget[]>([
    {
      id: 'entregas-hoje',
      titulo: 'Entregas Hoje',
      valor: 12,
      icone: 'inventory',
      cor: 'blue',
      tendencia: { valor: 8.2, tipo: 'aumento' },
      papeisPermitidos: [Papel.ADMIN, Papel.SINDICO, Papel.FUNCIONARIO],
    },
    {
      id: 'entregas-pendentes',
      titulo: 'Pendentes',
      valor: 5,
      icone: 'pending',
      cor: 'yellow',
      tendencia: { valor: 2.1, tipo: 'diminuicao' },
    },
    {
      id: 'entregas-entregues',
      titulo: 'Entregues',
      valor: 7,
      icone: 'check_circle',
      cor: 'green',
      tendencia: { valor: 12.5, tipo: 'aumento' },
    },
    {
      id: 'total-mes',
      titulo: 'Total do Mês',
      valor: 156,
      icone: 'trending_up',
      cor: 'purple',
      papeisPermitidos: [Papel.ADMIN, Papel.SINDICO],
    },
    {
      id: 'condominios-ativos',
      titulo: 'Condomínios',
      valor: 8,
      icone: 'apartment',
      cor: 'indigo',
      papeisPermitidos: [Papel.ADMIN],
    },
    {
      id: 'usuarios-online',
      titulo: 'Usuários Online',
      valor: 23,
      icone: 'people',
      cor: 'blue',
      papeisPermitidos: [Papel.ADMIN],
    },
  ]);

  public readonly widgetsPermitidos = computed(() => {
    const papelUsuario = this.autenticacaoService.papel();
    if (!papelUsuario) return [];

    return this.widgets().filter(widget => !widget.papeisPermitidos || widget.papeisPermitidos.includes(papelUsuario));
  });

  public readonly entregasRecentes = signal([
    {
      id: 1,
      descricao: 'Pacote da Amazon',
      morador: 'João Silva',
      tempo: '2 min atrás',
      status: 'Entregue',
    },
    {
      id: 2,
      descricao: 'Correspondência Bancária',
      morador: 'Maria Santos',
      tempo: '15 min atrás',
      status: 'Pendente',
    },
    {
      id: 3,
      descricao: 'Medicamentos',
      morador: 'Pedro Costa',
      tempo: '1 hora atrás',
      status: 'Notificado',
    },
  ]);

  public readonly atividadesRecentes = signal([
    {
      id: 1,
      tipo: 'entrega',
      icone: 'inventory',
      descricao: 'Nova entrega registrada para João Silva',
      tempo: '3 min atrás',
    },
    {
      id: 2,
      tipo: 'usuario',
      icone: 'person_add',
      descricao: 'Novo morador cadastrado no sistema',
      tempo: '1 hora atrás',
    },
    {
      id: 3,
      tipo: 'sistema',
      icone: 'settings',
      descricao: 'Backup automático realizado com sucesso',
      tempo: '2 horas atrás',
    },
  ]);

  public obterClassesIcone(cor: string): string {
    const classes = {
      blue: 'bg-blue-500',
      green: 'bg-green-500',
      yellow: 'bg-yellow-500',
      red: 'bg-red-500',
      purple: 'bg-purple-500',
      indigo: 'bg-indigo-500',
    };
    return classes[cor as keyof typeof classes] || 'bg-gray-500';
  }

  public obterClassesTexto(cor: string): string {
    const classes = {
      blue: 'text-blue-600',
      green: 'text-green-600',
      yellow: 'text-yellow-600',
      red: 'text-red-600',
      purple: 'text-purple-600',
      indigo: 'text-indigo-600',
    };
    return classes[cor as keyof typeof classes] || 'text-gray-600';
  }

  public obterClassesStatus(status: string): string {
    const classes = {
      Entregue: 'bg-green-100 text-green-800',
      Pendente: 'bg-yellow-100 text-yellow-800',
      Notificado: 'bg-blue-100 text-blue-800',
    };
    return classes[status as keyof typeof classes] || 'bg-gray-100 text-gray-800';
  }

  public obterClassesAtividade(tipo: string): string {
    const classes = {
      entrega: 'bg-blue-100 text-blue-600',
      usuario: 'bg-green-100 text-green-600',
      sistema: 'bg-purple-100 text-purple-600',
    };
    return classes[tipo as keyof typeof classes] || 'bg-gray-100 text-gray-600';
  }

  public obterPrimeiroNome(): string {
    const nomeCompleto = this.autenticacaoService.usuario()?.nome;
    return nomeCompleto?.split(' ')[0] || 'Usuário';
  }
}
