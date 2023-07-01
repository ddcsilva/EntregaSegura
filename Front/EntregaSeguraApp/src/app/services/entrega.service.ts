import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Entrega } from '@app/models/entrega.model';
import { TratamentoErrosService } from '@app/shared/services/tratamento-erros.service';
import { environment } from '@environments/environment';
import { Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EntregaService {
  private urlBaseApi = environment.urlBaseApi;

  constructor(
    private httpClient: HttpClient,
    private tratamentoErrosService: TratamentoErrosService
  ) { }

  public obterEntregas(): Observable<Entrega[]> {
    const url = `${this.urlBaseApi}/entregas`;
    return this.fazerRequisicao(() => this.httpClient.get<Entrega[]>(url));
  }

  public obterEntregaPorId(id: number): Observable<Entrega> {
    const url = `${this.urlBaseApi}/entregas/${id}`;
    return this.fazerRequisicao(() => this.httpClient.get<Entrega>(url));
  }

  public criar(entrega: Entrega): Observable<Entrega> {
    const url = `${this.urlBaseApi}/entregas`;
    return this.fazerRequisicao(() => this.httpClient.post<Entrega>(url, entrega));
  }

  public atualizar(id: number, entrega: Entrega): Observable<Entrega> {
    const url = `${this.urlBaseApi}/entregas/${id}`;
    return this.fazerRequisicao(() => this.httpClient.put<Entrega>(url, entrega));
  }

  public excluir(id: number): Observable<void> {
    const url = `${this.urlBaseApi}/entregas/${id}`;
    return this.fazerRequisicao(() => this.httpClient.delete<void>(url));
  }

  public confirmarRetirada(id: number): Observable<void> {
    const url = `${this.urlBaseApi}/entregas/confirmar-retirada/${id}`;
    return this.fazerRequisicao(() => this.httpClient.put<void>(url, null));
  }

  public notificarEntrega(id: number): Observable<void> {
    const url = `${this.urlBaseApi}/entregas/notificar-entrega/${id}`;
    return this.fazerRequisicao(() => this.httpClient.put<void>(url, null));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}
