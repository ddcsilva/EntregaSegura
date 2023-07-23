import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TratamentoErrosService } from '../shared/services/tratamento-erros.service';
import { Transportadora } from '../models/transportadora.model';

@Injectable({
  providedIn: 'root'
})
export class TransportadoraService {
  private urlBaseApi = `${environment.urlBaseApi}/api`;

  constructor(
    private httpClient: HttpClient,
    private tratamentoErrosService: TratamentoErrosService
  ) { }

  public obterTransportadoras(): Observable<Transportadora[]> {
    const url = `${this.urlBaseApi}/transportadoras`;
    return this.fazerRequisicao(() => this.httpClient.get<Transportadora[]>(url));
  }

  public obterTransportadoraPorId(id: number): Observable<Transportadora> {
    const url = `${this.urlBaseApi}/transportadoras/${id}`;
    return this.fazerRequisicao(() => this.httpClient.get<Transportadora>(url));
  }

  public criar(transportadora: Transportadora): Observable<Transportadora> {
    const url = `${this.urlBaseApi}/transportadoras`;
    return this.fazerRequisicao(() => this.httpClient.post<Transportadora>(url, transportadora));
  }

  public atualizar(id: number, transportadora: Transportadora): Observable<Transportadora> {
    const url = `${this.urlBaseApi}/transportadoras/${id}`;
    return this.fazerRequisicao(() => this.httpClient.put<Transportadora>(url, transportadora));
  }

  public excluir(id: number): Observable<void> {
    const url = `${this.urlBaseApi}/transportadoras/${id}`;
    return this.fazerRequisicao(() => this.httpClient.delete<void>(url));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}
