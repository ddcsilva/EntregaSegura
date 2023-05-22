import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Condominio } from '@app/models/Condominio';
import { Observable, catchError, retry } from 'rxjs';
import { TratamentoErrosService } from './tratamento-erros.service';

@Injectable()
export class CondominioService {
  urlBase: string = 'https://localhost:5001/api/condominios';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private errorHandlerService: TratamentoErrosService) { }

  public obterTodos(): Observable<Condominio[]> {
    return this.fazerRequisicao(() => this.http.get<Condominio[]>(this.urlBase));
  }

  public obterPorId(id: string): Observable<Condominio> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.get<Condominio>(url));
  }

  public criar(condominio: Condominio): Observable<Condominio> {
    return this.fazerRequisicao(() => this.http.post<Condominio>(this.urlBase, condominio, this.httpOptions));
  }

  public atualizar(id: string, condominio: Condominio): Observable<Condominio> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.put<Condominio>(url, condominio, this.httpOptions));
  }

  public excluir(id: string): Observable<void> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.delete<void>(url));
  }  

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.errorHandlerService.tratarErro)
    );
  }
}
