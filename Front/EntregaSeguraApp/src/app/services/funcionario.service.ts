import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TratamentoErrosService } from '../shared/services/tratamento-erros.service';
import { Observable, catchError } from 'rxjs';
import { Funcionario } from '../models/funcionario.model';

@Injectable({
  providedIn: 'root'
})
export class FuncionarioService {
  private urlBaseApi = `${environment.urlBaseApi}/api`;

  constructor(
    private httpClient: HttpClient,
    private tratamentoErrosService: TratamentoErrosService
  ) { }

  public obterFuncionarios(): Observable<Funcionario[]> {
    const url = `${this.urlBaseApi}/funcionarios`;
    return this.fazerRequisicao(() => this.httpClient.get<Funcionario[]>(url));
  }

  public obterFuncionarioPorId(id: number): Observable<Funcionario> {
    const url = `${this.urlBaseApi}/funcionarios/${id}`;
    return this.fazerRequisicao(() => this.httpClient.get<Funcionario>(url));
  }

  public criar(funcionario: Funcionario): Observable<Funcionario> {
    const url = `${this.urlBaseApi}/funcionarios`;
    return this.fazerRequisicao(() => this.httpClient.post<Funcionario>(url, funcionario));
  }

  public atualizar(id: number, funcionario: Funcionario): Observable<Funcionario> {
    const url = `${this.urlBaseApi}/funcionarios/${id}`;
    return this.fazerRequisicao(() => this.httpClient.put<Funcionario>(url, funcionario));
  }

  public excluir(id: number): Observable<void> {
    const url = `${this.urlBaseApi}/funcionarios/${id}`;
    return this.fazerRequisicao(() => this.httpClient.delete<void>(url));
  }

  private fazerRequisicao(operacaoHttp: Function): Observable<any> {
    return operacaoHttp().pipe(
      catchError(this.tratamentoErrosService.tratarErro.bind(this.tratamentoErrosService))
    );
  }
}
