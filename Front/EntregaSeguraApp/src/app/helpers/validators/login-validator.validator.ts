import { AbstractControl, ValidatorFn } from '@angular/forms';

export function LoginValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const valor = control.value;
    const emailValidoRegex = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;
    return valor === 'admin' || emailValidoRegex.test(valor)
      ? null
      : { loginInvalido: { value: control.value } };
  };
}
