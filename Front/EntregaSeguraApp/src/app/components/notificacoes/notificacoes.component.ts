import { Component, OnInit } from '@angular/core';
import { Notificacao } from 'src/app/models/notificacao';
import { NotificacaoService } from 'src/app/services/notificacao/notificacao.service';

@Component({
  selector: 'app-notificacoes',
  templateUrl: './notificacoes.component.html',
  styleUrls: ['./notificacoes.component.scss']
})
export class NotificacoesComponent implements OnInit {

  notificacoes: Notificacao[] = [];
  quantidadeNotificacoes: number = 0;

  constructor(private notificacaoService: NotificacaoService) { }

  ngOnInit(): void {
    this.notificacaoService.getNotifications().subscribe(data => {
      this.notificacoes = data;
      this.quantidadeNotificacoes = data.length;
    });
  }
}
