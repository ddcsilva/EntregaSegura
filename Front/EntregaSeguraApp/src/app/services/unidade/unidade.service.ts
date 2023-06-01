import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Unidade } from 'src/app/models/unidade';
import { UnidadesEmMassa } from 'src/app/models/unidadesEmMassa';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Injectable()
export class UnidadeService {
  private urlBase: string = 'https://localhost:5001/api/unidades';

  private httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private tratamentoErrosService: TratamentoErrosService) { }

  public adicionarEmMassa(unidades: UnidadesEmMassa): Observable<void> {
    const url = `${this.urlBase}/em-massa`;
    return this.fazerRequisicao(() => this.http.post<void>(url, unidades, this.httpOptions));
  }

  public criar(unidade: Unidade): Observable<Unidade> {
    return this.fazerRequisicao(() => this.http.post<Unidade>(this.urlBase, unidade, this.httpOptions));
  }

  public atualizar(id: number, unidade: Unidade): Observable<Unidade> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.put<Unidade>(url, unidade, this.httpOptions));
  }

  public excluir(id: number): Observable<void> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.delete<void>(url));
  }

  public obterTodos(): Observable<Unidade[]> {
    return this.fazerRequisicao(() => this.http.get<Unidade[]>(this.urlBase));
  }

  public obterPorId(id: number): Observable<Unidade> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.get<Unidade>(url));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}
