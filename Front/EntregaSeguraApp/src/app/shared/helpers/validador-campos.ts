import { AbstractControl, FormGroup } from "@angular/forms";

export class ValidadorCampos {
    static DeveSerIgual(controlName: string, controlNameComparacao: string): any {
        return (abstractControl: AbstractControl) => {
            const formGroup = abstractControl as FormGroup;
            const control = formGroup.controls[controlName];
            const controlComparacao = formGroup.controls[controlNameComparacao];

            if (controlComparacao.errors && !controlComparacao.errors['deveSerIgual']) {
                return null;
            }

            if (control.value !== controlComparacao.value) {
                controlComparacao.setErrors({ deveSerIgual: true });
            } else {
                controlComparacao.setErrors(null);
            }

            return null;
        };
    }

    static ValidaCNPJ(control: AbstractControl): { [key: string]: boolean } | null {
        const cnpj = control.value;
        if (cnpj) {
            const val = cnpj.replace(/\D/g, '');

            if (val.length !== 14) {
                return { 'cnpjInvalido': true };
            }

            let tamanho = val.length - 2;
            let numeros = val.substring(0, tamanho);
            const digitos = val.substring(tamanho);
            let soma = 0;
            let pos = tamanho - 7;

            for (let i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) {
                    pos = 9;
                }
            }

            let resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

            if (resultado !== parseInt(digitos.charAt(0), 10)) {
                return { 'cnpjInvalido': true };
            }

            tamanho += 1;
            numeros = val.substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;

            for (let i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) {
                    pos = 9;
                }
            }

            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

            if (resultado !== parseInt(digitos.charAt(1), 10)) {
                return { 'cnpjInvalido': true };
            }
        }

        return null;
    }

    static ValidaCPF(control: AbstractControl): { [key: string]: boolean } | null {
        const cpf = control.value;
        if (cpf) {
            const val = cpf.replace(/\D/g, '');
    
            if (val.length !== 11) {
                return { 'cpfInvalido': true };
            }
    
            if (!val.match(/^[0-9]+$/)) {
                return { 'cpfInvalido': true };
            }
    
            let tempCpf = val.substring(0, 9);
            let soma = 0;
            const multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
    
            for (let i = 0; i < 9; i++) {
                soma += parseInt(tempCpf.charAt(i), 10) * multiplicador1[i];
            }
    
            let resto = (soma % 11);
            if (resto < 2) {
                resto = 0;
            } else {
                resto = 11 - resto;
            }
    
            let digito = resto.toString();
            tempCpf = tempCpf + digito;
            soma = 0;
    
            const multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
            for (let i = 0; i < 10; i++) {
                soma += parseInt(tempCpf.charAt(i), 10) * multiplicador2[i];
            }
    
            resto = (soma % 11);
            if (resto < 2) {
                resto = 0;
            } else {
                resto = 11 - resto;
            }
    
            digito = digito + resto.toString();
    
            if (val.slice(-2) !== digito) {
                return { 'cpfInvalido': true };
            }
        }
    
        return null;
    }    
}