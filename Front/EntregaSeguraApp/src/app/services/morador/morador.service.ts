import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Morador } from 'src/app/models/morador';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Injectable()
@Injectable()
export class MoradorService {
  private urlBase: string = 'https://localhost:5001/api/moradores';

  private httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private tratamentoErrosService: TratamentoErrosService) { }

  public obterTodos(): Observable<Morador[]> {
    return of([
      {
        id: 1,
        nome: "João Silva",
        email: "joao.silva@example.com",
        telefone: "(11) 98765-4321",
        ramal: "1234",
        nomeCondominio: "Condomínio Bela Vista",
        blocoAndarUnidade: "Bloco A, 10º andar, Unidade 2"
      },
      {
        id: 2,
        nome: "Maria Santos",
        email: "maria.santos@example.com",
        telefone: "(11) 98765-4322",
        ramal: "1235",
        nomeCondominio: "Condomínio Bela Vista",
        blocoAndarUnidade: "Bloco A, 10º andar, Unidade 3"
      }
    ]);
  }
}

