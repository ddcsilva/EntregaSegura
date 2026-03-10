import { Injectable, computed, signal, inject } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { toSignal } from '@angular/core/rxjs-interop';

import { AutenticacaoService } from '@core/services';
import { Papel } from '@core/models';

export interface ItemMenu {
  label: string;
  icone: string;
  rota: string;
  papeisPermitidos: Papel[];
  filhos?: ItemMenu[];
  badge?: string;
}

@Injectable({
  providedIn: 'root',
})
export class NavegacaoService {
  private readonly breakpointObserver = inject(BreakpointObserver);
  private readonly autenticacaoService = inject(AutenticacaoService);

  private readonly sidebarAberta = signal<boolean>(false);
  private readonly menuColapsado = signal<boolean>(false);

  private readonly breakpointMobile = toSignal(
    this.breakpointObserver.observe([Breakpoints.Handset, Breakpoints.TabletPortrait]),
    { initialValue: { matches: false, breakpoints: {} } as BreakpointState }
  );

  public readonly sidebar = this.sidebarAberta.asReadonly();
  public readonly menu = this.menuColapsado.asReadonly();
  public readonly mobile = computed(() => this.breakpointMobile()?.matches ?? false);

  private readonly ITENS_MENU: ItemMenu[] = [
    {
      label: 'Dashboard',
      icone: 'dashboard',
      rota: '/dashboard',
      papeisPermitidos: [Papel.ADMIN, Papel.SINDICO, Papel.FUNCIONARIO, Papel.MORADOR],
    },
    {
      label: 'Entregas',
      icone: 'inventory',
      rota: '/entregas',
      papeisPermitidos: [Papel.ADMIN, Papel.SINDICO, Papel.FUNCIONARIO, Papel.MORADOR],
    },
    {
      label: 'Condomínios',
      icone: 'apartment',
      rota: '/condominios',
      papeisPermitidos: [Papel.ADMIN, Papel.SINDICO],
    },
    {
      label: 'Moradores',
      icone: 'people',
      rota: '/moradores',
      papeisPermitidos: [Papel.ADMIN, Papel.SINDICO],
    },
    {
      label: 'Funcionários',
      icone: 'work',
      rota: '/funcionarios',
      papeisPermitidos: [Papel.ADMIN],
    },
    {
      label: 'Transportadoras',
      icone: 'local_shipping',
      rota: '/transportadoras',
      papeisPermitidos: [Papel.ADMIN, Papel.SINDICO, Papel.FUNCIONARIO],
    },
    {
      label: 'Administração',
      icone: 'admin_panel_settings',
      rota: '/admin',
      papeisPermitidos: [Papel.ADMIN],
      filhos: [
        {
          label: 'Usuários',
          icone: 'person',
          rota: '/admin/usuarios',
          papeisPermitidos: [Papel.ADMIN],
        },
        {
          label: 'Configurações',
          icone: 'settings',
          rota: '/admin/configuracoes',
          papeisPermitidos: [Papel.ADMIN],
        },
      ],
    },
  ];

  public readonly itensMenuPermitidos = computed(() => {
    const papelUsuario = this.autenticacaoService.papel();

    if (!papelUsuario) return [];

    return this.ITENS_MENU.filter(item => item.papeisPermitidos.includes(papelUsuario)).map(item => ({
      ...item,
      filhos: item.filhos?.filter(filho => filho.papeisPermitidos.includes(papelUsuario)),
    }));
  });

  public alternarSidebar(): void {
    this.sidebarAberta.update(aberta => !aberta);
  }

  public fecharSidebar(): void {
    this.sidebarAberta.set(false);
  }

  public abrirSidebar(): void {
    this.sidebarAberta.set(true);
  }

  public alternarCollapseMenu(): void {
    this.menuColapsado.update(colapsado => !colapsado);
  }

  public navegarEFecharSidebar(): void {
    if (this.mobile()) {
      this.fecharSidebar();
    }
  }

  public verificarRotaAtiva(rota: string, urlAtual: string): boolean {
    if (rota === '/dashboard') {
      return urlAtual === '/dashboard' || urlAtual === '/';
    }
    return urlAtual.startsWith(rota);
  }
}
