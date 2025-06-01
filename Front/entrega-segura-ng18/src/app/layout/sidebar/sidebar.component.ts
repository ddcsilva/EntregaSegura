import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

import { NavegacaoService } from '../services/navegacao.service';
import { AutenticacaoService } from '@core/services';

@Component({
  selector: 'es-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent {
  public readonly navegacaoService = inject(NavegacaoService);
  public readonly autenticacaoService = inject(AutenticacaoService);
  private readonly router = inject(Router);

  private gruposExpandidos = new Set<string>(['Administração']);

  public alternarGrupo(labelGrupo: string): void {
    if (this.gruposExpandidos.has(labelGrupo)) {
      this.gruposExpandidos.delete(labelGrupo);
    } else {
      this.gruposExpandidos.add(labelGrupo);
    }
  }

  public verificarGrupoExpandido(labelGrupo: string): boolean {
    return this.gruposExpandidos.has(labelGrupo);
  }

  public obterIniciais(): string {
    const nome = this.autenticacaoService.usuario()?.nome;
    if (!nome) return 'U';

    const nomes = nome.split(' ');
    const iniciais = nomes.length > 1 ? `${nomes[0][0]}${nomes[nomes.length - 1][0]}` : nomes[0][0];

    return iniciais.toUpperCase();
  }
}
