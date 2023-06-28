import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TratamentoErrosService } from '../shared/services/tratamento-erros.service';
import { Observable, catchError } from 'rxjs';
import { Morador } from '../models/morador.model';

@Injectable({
  providedIn: 'root'
})
export class MoradorService {
  private urlBaseApi = environment.urlBaseApi;

  constructor(
    private httpClient: HttpClient,
    private tratamentoErrosService: TratamentoErrosService
  ) { }

  public obterMoradores(): Observable<Morador[]> {
    const url = `${this.urlBaseApi}/moradores`;
    return this.fazerRequisicao(() => this.httpClient.get<Morador[]>(url));
  }

  public obterMoradorPorId(id: number): Observable<Morador> {
    const url = `${this.urlBaseApi}/moradores/${id}`;
    return this.fazerRequisicao(() => this.httpClient.get<Morador>(url));
  }

  public criar(morador: Morador): Observable<Morador> {
    const url = `${this.urlBaseApi}/moradores`;
    return this.fazerRequisicao(() => this.httpClient.post<Morador>(url, morador));
  }

  public atualizar(id: number, morador: Morador): Observable<Morador> {
    const url = `${this.urlBaseApi}/moradores/${id}`;
    return this.fazerRequisicao(() => this.httpClient.put<Morador>(url, morador));
  }

  public excluir(id: number): Observable<void> {
    const url = `${this.urlBaseApi}/moradores/${id}`;
    return this.fazerRequisicao(() => this.httpClient.delete<void>(url));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }

}
