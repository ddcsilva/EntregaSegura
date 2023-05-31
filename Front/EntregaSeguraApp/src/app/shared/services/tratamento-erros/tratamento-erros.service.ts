import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable()
export class TratamentoErrosService {
  public tratarErro(erro: HttpErrorResponse) {
    let mensagemErro: string;
  
    if (this.apiEstaForaDoAr(erro)) {
      mensagemErro = 'Nosso serviço está temporariamente indisponível. Por favor, tente novamente mais tarde.';
    } else if (this.erroDeClienteOuRede(erro)) {
      mensagemErro = erro.error.message;
    } else if (this.erroDoBackendComCorpoResposta(erro)) {
      mensagemErro = erro.error.errors;
    } else {
      mensagemErro = `${erro.message}`;
    }
  
    return throwError(() => new Error(mensagemErro));
  }
  

  private apiEstaForaDoAr(erro: HttpErrorResponse): boolean {
    return erro.status === 0;
  }

  private erroDeClienteOuRede(erro: HttpErrorResponse): boolean {
    return erro.error instanceof ErrorEvent;
  }

  private erroDoBackendComCorpoResposta(erro: HttpErrorResponse): boolean {
    return erro.error && erro.error.errors;
  }
}


