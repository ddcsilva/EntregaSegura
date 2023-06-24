import { Component, OnInit } from '@angular/core';

import { Notificacao } from '@app/models';
import { NotificacaoService } from '@app/services';
import { AutenticacaoService } from '@app/services';
import { SidenavService } from '@app/shared/services/';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  notificacoes: Notificacao[] = [];
  quantidadeNotificacoes: number = 0;

  constructor(private notificacaoService: NotificacaoService, private sidenavService: SidenavService, public autenticacaoService : AutenticacaoService) { }

  ngOnInit(): void {
    this.notificacaoService.getNotifications().subscribe(data => {
      this.notificacoes = data;
      this.quantidadeNotificacoes = data.length;
    });
  }

  toggleSidenav(): void {
    this.sidenavService.toggle();
  }
}
