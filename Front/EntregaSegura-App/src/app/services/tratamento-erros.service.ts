import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable()
export class TratamentoErrosService {

  public tratarErro(erro: HttpErrorResponse) {
    if (erro.error instanceof ErrorEvent) {
      return throwError(() => new Error(erro.error.message));
    } else {
      if (erro.error && erro.error.errors) {
        return throwError(() => erro.error.errors);
      } else {
        const mensagemErro = `Código do Erro: ${erro.status}\nMensagem: ${erro.message}`;
        return throwError(() => new Error(mensagemErro));
      }
    }
  }
}

