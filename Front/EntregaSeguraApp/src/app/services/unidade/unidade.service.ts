// Angular imports
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

// Library imports
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

// Models
import { Unidade } from 'src/app/models/unidade';
import { UnidadesEmMassa } from 'src/app/models/unidades-em-massa';
import { ApiResponse } from 'src/app/shared/models/api-response';

// Services
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Injectable()
export class UnidadeService {
  private urlBase: string = 'https://localhost:5001/api/unidades';

  private httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private tratamentoErrosService: TratamentoErrosService) { }

  public obterTodos(): Observable<Unidade[]> {
    return this.fazerRequisicao(() => this.http.get<ApiResponse<Unidade[]>>(this.urlBase))
      .pipe(
        map(response => response.data)
      );
  }

  public obterTodosPorCondominioId(condominioId: number): Observable<Unidade[]> {
    const url = `${this.urlBase}/por-condominio/${condominioId}`;
    return this.fazerRequisicao(() => this.http.get<ApiResponse<Unidade[]>>(url))
      .pipe(
        map(response => response.data)
      );
  }

  public obterPorId(id: string): Observable<Unidade> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.get<ApiResponse<Unidade>>(url))
      .pipe(map(response => response.data));
  }

  public criar(unidade: Unidade): Observable<Unidade> {
    return this.fazerRequisicao(() => this.http.post<ApiResponse<Unidade>>(this.urlBase, unidade, this.httpOptions))
      .pipe(map(response => response.data));
  }

  public atualizar(id: string, unidade: Unidade): Observable<Unidade> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.put<ApiResponse<Unidade>>(url, unidade, this.httpOptions))
      .pipe(map(response => response.data));
  }

  public excluir(id: number): Observable<void> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.delete<void>(url));
  }

  public adicionarEmMassa(unidades: UnidadesEmMassa): Observable<void> {
    const url = `${this.urlBase}/em-massa`;
    return this.fazerRequisicao(() => this.http.post<void>(url, unidades, this.httpOptions));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}
