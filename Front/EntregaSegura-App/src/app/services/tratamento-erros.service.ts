import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable()
export class TratamentoErrosService {

  public tratarErro(erro: HttpErrorResponse) {
  if (erro.error instanceof ErrorEvent) {
    // Um erro do lado do cliente ou problema de rede ocorreu. Trate-o de acordo.
    console.error('Ocorreu um erro:', erro.error.message);
    return throwError(erro.error.message);
  } else {
    // O backend retornou um código de resposta malsucedido.
    // O corpo da resposta pode conter pistas sobre o que deu errado.
    if (erro.error && erro.error.errors) {
      return throwError(erro.error.errors);
    } else {
      const mensagemErro = `Código do Erro: ${erro.status}\nMensagem: ${erro.message}`;
      return throwError(mensagemErro);
    }
  }
}

}

