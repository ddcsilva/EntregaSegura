import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UnidadesEmMassa } from 'src/app/models/unidadesEmMassa';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Injectable()
export class UnidadeService {
  private urlBase: string = 'https://localhost:5001/api/unidades';
  
  private httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private tratamentoErrosService: TratamentoErrosService) { }

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
