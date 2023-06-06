import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatarCnpj'
})
export class FormatarCnpjPipe implements PipeTransform {
  transform(cnpj: string): string {
    let cnpjFormatado = cnpj.replace(/\D/g, '');

    if (cnpjFormatado.length === 14) {
      cnpjFormatado = cnpjFormatado.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5');
    }

    return cnpjFormatado;
  }
}