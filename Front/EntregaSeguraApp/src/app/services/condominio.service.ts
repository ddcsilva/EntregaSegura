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
    return this.fazerRequisicao(() => this.httpClient.get<Condominio[]>(`${this.urlBaseApi}/condominios`));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}
