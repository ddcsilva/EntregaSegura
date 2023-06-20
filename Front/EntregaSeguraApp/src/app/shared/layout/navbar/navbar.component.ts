import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Notificacao } from 'src/app/models/notificacao';
import { NotificacaoService } from 'src/app/services/notificacao/notificacao.service';
import { SidenavService } from '../../services/sidenav-service.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  notificacoes: Notificacao[] = [];
  quantidadeNotificacoes: number = 0;

  constructor(private notificacaoService: NotificacaoService, private sidenavService: SidenavService) { }

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
