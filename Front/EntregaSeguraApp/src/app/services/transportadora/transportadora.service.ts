// Angular imports
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

// Library imports
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

// Models
import { Transportadora } from 'src/app/models/transportadora';

// Services
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Injectable()
export class TransportadoraService {
  private urlBase: string = 'https://localhost:5001/api/transportadoras';
  
  private httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private tratamentoErrosService: TratamentoErrosService) { }

  public criar(transportadora: Transportadora): Observable<Transportadora> {
    return this.fazerRequisicao(() => this.http.post<Transportadora>(this.urlBase, transportadora, this.httpOptions));
  }

  public atualizar(id: string, transportadora: Transportadora): Observable<Transportadora> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.put<Transportadora>(url, transportadora, this.httpOptions));
  }

  public excluir(id: string): Observable<void> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.delete<void>(url));
  }

  public obterTodos(): Observable<Transportadora[]> {
    return this.fazerRequisicao(() => this.http.get<Transportadora[]>(this.urlBase));
  }

  public obterPorId(id: string): Observable<Transportadora> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.get<Transportadora>(url));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}
