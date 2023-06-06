// Angular imports
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

// Library imports
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

// Models
import { Transportadora } from 'src/app/models/transportadora';
import { ApiResponse } from 'src/app/shared/models/api-response';

// Services
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Injectable()
export class TransportadoraService {
  private urlBase: string = 'https://localhost:5001/api/transportadoras';

  private httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private tratamentoErrosService: TratamentoErrosService) { }

  public obterTodos(): Observable<Transportadora[]> {
    return this.fazerRequisicao(() => this.http.get<ApiResponse<Transportadora[]>>(this.urlBase))
      .pipe(
        map(response => response.data)
      );
  }

  public obterPorId(id: string): Observable<Transportadora> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.get<ApiResponse<Transportadora>>(url))
      .pipe(map(response => response.data));
  }

  public criar(transportadora: Transportadora): Observable<Transportadora> {
    return this.fazerRequisicao(() => this.http.post<ApiResponse<Transportadora>>(this.urlBase, transportadora, this.httpOptions))
      .pipe(map(response => response.data));
  }

  public atualizar(id: string, transportadora: Transportadora): Observable<Transportadora> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.put<ApiResponse<Transportadora>>(url, transportadora, this.httpOptions))
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
