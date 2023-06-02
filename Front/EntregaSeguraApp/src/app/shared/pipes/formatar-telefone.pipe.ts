import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatarTelefone'
})
export class FormatarTelefonePipe implements PipeTransform {
  transform(numero: string): string {
    let telefoneFormatado = numero.replace(/\D/g, '');

    if (telefoneFormatado.length === 10) {
      telefoneFormatado = telefoneFormatado.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
    } else if (telefoneFormatado.length === 11) {
      telefoneFormatado = telefoneFormatado.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
    }

    return telefoneFormatado;
  }
}
