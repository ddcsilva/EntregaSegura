import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Notificacao } from 'src/app/models/notificacao';

@Injectable({
  providedIn: 'root'
})
export class NotificacaoService {

  constructor() { }

  getNotifications(): Observable<Notificacao[]> {
    const notificacoes: Notificacao[] = [
      {
        transportadora: 'Empresa de Entregas 1',
        dataRecebimento: new Date(2023, 5, 14)
      },
      {
        transportadora: 'Empresa de Entregas 2',
        dataRecebimento: new Date(2023, 5, 13)
      }
    ];

    return of(notificacoes);
  }
}
