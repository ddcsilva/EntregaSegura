import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { AutenticacaoService } from '@core/services';
import { NavegacaoService } from '../services/navegacao.service';

@Component({
  selector: 'es-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
})
export class HeaderComponent {
  public readonly autenticacaoService = inject(AutenticacaoService);
  public readonly navegacaoService = inject(NavegacaoService);
  private readonly router = inject(Router);

  public readonly menuUsuarioAberto = signal<boolean>(false);

  public obterIniciais(): string {
    const nome = this.autenticacaoService.usuario()?.nome;
    if (!nome) return 'U';

    const nomes = nome.split(' ');
    const iniciais = nomes.length > 1 ? `${nomes[0][0]}${nomes[nomes.length - 1][0]}` : nomes[0][0];

    return iniciais.toUpperCase();
  }

  public alternarMenuUsuario(): void {
    this.menuUsuarioAberto.update(aberto => !aberto);
  }

  public fecharMenuUsuario(): void {
    this.menuUsuarioAberto.set(false);
  }

  public efetuarLogout(): void {
    this.fecharMenuUsuario();
    this.autenticacaoService.logout();
  }

  public navegarPara(rota: string): void {
    this.fecharMenuUsuario();
    this.router.navigate([rota]);
  }
}
