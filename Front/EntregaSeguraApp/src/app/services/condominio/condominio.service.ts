// Angular imports
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

// Library imports
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

// Models
import { Condominio } from 'src/app/models/condominio';
import { ApiResponse } from 'src/app/shared/models/api-response';

// Services
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Injectable()
export class CondominioService {
  private urlBase: string = 'https://localhost:5001/api/condominios';
  private tokenHeader = new HttpHeaders({ 'Authorization': `Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6ImFkbWluIiwiZW1haWwiOiJhZG1pbkBsb2NhbGhvc3QiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2ODcxMTc3MDgsImV4cCI6MTY4NzE0NjUwOCwiaWF0IjoxNjg3MTE3NzA4fQ.bvJ21mHP1XG1KsH71cSG8aqqVMK-HGxQiPYoLtvWQsca6GDxQWBIHhoBgdyU5wO-AD5Fe210RAMv81S2av3LKw` }); 

  private httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private tratamentoErrosService: TratamentoErrosService) { }

  public obterTodos(): Observable<Condominio[]> {
    return this.fazerRequisicao(() => this.http.get<ApiResponse<Condominio[]>>(this.urlBase, { headers: this.tokenHeader }))
      .pipe(
        map(response => response.data)
      );
  }

  public obterPorId(id: string): Observable<Condominio> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.get<ApiResponse<Condominio>>(url))
      .pipe(map(response => response.data));
  }

  public criar(condominio: Condominio): Observable<Condominio> {
    return this.fazerRequisicao(() => this.http.post<ApiResponse<Condominio>>(this.urlBase, condominio, this.httpOptions))
      .pipe(map((response: ApiResponse<Condominio>) => response.data));
  }

  public atualizar(id: string, condominio: Condominio): Observable<Condominio> {
    const url = `${this.urlBase}/${id}`;
    return this.fazerRequisicao(() => this.http.put<ApiResponse<Condominio>>(url, condominio, this.httpOptions))
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