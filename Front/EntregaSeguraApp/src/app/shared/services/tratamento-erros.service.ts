import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TratamentoErrosService {

  constructor() { }

  public tratarErro(errorResponse: HttpErrorResponse | Error): Observable<never> {
    let mensagemErro: string;

    if (errorResponse instanceof HttpErrorResponse) {
      if (this.apiEstaForaDoAr(errorResponse)) {
        mensagemErro = 'Nosso serviço está temporariamente indisponível. Por favor, tente novamente mais tarde.';
      } else if (this.erroDeClienteOuRede(errorResponse)) {
        mensagemErro = errorResponse.error.message;
      } else if (this.erroDoBackendComCorpoResposta(errorResponse)) {
        mensagemErro = errorResponse.error.errors;
      } else {
        mensagemErro = `${errorResponse.message}`;
      }
    } else {
      mensagemErro = `${errorResponse.message}`;
    }

    return throwError(() => new Error(mensagemErro));
  }

  private apiEstaForaDoAr(errorResponse: HttpErrorResponse): boolean {
    return errorResponse.status === 0;
  }

  private erroDeClienteOuRede(errorResponse: HttpErrorResponse): boolean {
    return errorResponse.error instanceof ErrorEvent;
  }

  private erroDoBackendComCorpoResposta(errorResponse: HttpErrorResponse): boolean {
    return errorResponse.error && Array.isArray(errorResponse.error.errors);
  }
}
