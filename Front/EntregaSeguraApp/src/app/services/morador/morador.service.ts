// Angular imports
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

// Library imports
import { Observable, catchError, map } from 'rxjs';

// Models
import { Morador } from 'src/app/models/morador';
import { ApiResponse } from 'src/app/shared/models/api-response';

// Services
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Injectable()
export class MoradorService {
  private urlBase: string = 'https://localhost:5001/api/moradores';
  

  private httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private tratamentoErrosService: TratamentoErrosService) { }

  public obterTodos(): Observable<Morador[]> {
    return this.fazerRequisicao(() => this.http.get<ApiResponse<Morador[]>>(this.urlBase))
      .pipe(
        map(response => response.data)
      );
  }

  public obterPorId(id: string): Observable<Morador> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.get<ApiResponse<Morador>>(url))
      .pipe(map(response => response.data));
  }

  public criar(morador: Morador): Observable<Morador> {
    return this.fazerRequisicao(() => this.http.post<ApiResponse<Morador>>(this.urlBase, morador, this.httpOptions))
      .pipe(map((response: ApiResponse<Morador>) => response.data));
  }

  public atualizar(id: string, morador: Morador): Observable<Morador> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.put<ApiResponse<Morador>>(url, morador, this.httpOptions))
      .pipe(map(response => response.data));
  }

  public excluir(id: string): Observable<void> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.delete<void>(url));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}

