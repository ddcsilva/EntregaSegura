import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TratamentoErrosService } from '../shared/services/tratamento-erros.service';
import { Observable, catchError } from 'rxjs';
import { UnidadesEmMassa } from '../models/unidades-em-massa.model';

@Injectable({
  providedIn: 'root'
})
export class UnidadeService {
  private urlBaseApi = environment.urlBaseApi;

  constructor(
    private httpClient: HttpClient,
    private tratamentoErrosService: TratamentoErrosService
  ) { }

  public adicionarEmMassa(unidades: UnidadesEmMassa): Observable<void> {
    const url = `${this.urlBaseApi}/unidades/adicionar-em-massa`;
    return this.fazerRequisicao(() => this.httpClient.post<void>(url, unidades));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}
