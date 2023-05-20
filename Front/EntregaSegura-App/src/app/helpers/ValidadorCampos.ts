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
}