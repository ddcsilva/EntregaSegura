import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  formulario: FormGroup = new FormGroup({});

  get formControl(): any {
    return this.formulario.controls;
  }

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.validarFormulario();
  }

  private validarFormulario(): void {
    this.formulario = this.formBuilder.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  public gerarClassesValidacao(campoFormulario: FormControl): any {
    return {
      'is-invalid': campoFormulario.errors && campoFormulario.touched,
      'is-valid': !campoFormulario.errors && campoFormulario.touched
    };
  }
}
