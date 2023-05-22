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
}