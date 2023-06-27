import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Condominio } from '../models/condominio.model';
import { TratamentoErrosService } from '../shared/services/tratamento-erros.service';

@Injectable({
  providedIn: 'root'
})
export class CondominioService {
  private urlBaseApi = environment.urlBaseApi;

  constructor(
    private httpClient: HttpClient,
    private tratamentoErrosService: TratamentoErrosService
  ) { }

  public obterCondominios(): Observable<Condominio[]> {
    const url = `${this.urlBaseApi}/condominios`;
    return this.fazerRequisicao(() => this.httpClient.get<Condominio[]>(url));
  }

  public ObterCondominioPorId(id: number): Observable<Condominio> {
    const url = `${this.urlBaseApi}/condominios/${id}`;
    return this.fazerRequisicao(() => this.httpClient.get<Condominio>(url));
  }

  public criar(condominio: Condominio): Observable<Condominio> {
    const url = `${this.urlBaseApi}/condominios`;
    return this.fazerRequisicao(() => this.httpClient.post<Condominio>(url, condominio));
  }

  public atualizar(id: number, condominio: Condominio): Observable<Condominio> {
    const url = `${this.urlBaseApi}/condominios/${id}`;
    return this.fazerRequisicao(() => this.httpClient.put<Condominio>(url, condominio));
  }

  public excluir(id: number): Observable<void> {
    const url = `${this.urlBaseApi}/condominios/${id}`;
    return this.fazerRequisicao(() => this.httpClient.delete<void>(url));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}
