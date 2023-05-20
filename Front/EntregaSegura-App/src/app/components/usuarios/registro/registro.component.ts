import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidadorCampos } from '@app/helpers/ValidadorCampos';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.scss']
})
export class RegistroComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }
  formulario: FormGroup = new FormGroup({});

  get formControl(): any {
    return this.formulario.controls;
  }

  ngOnInit(): void {
    this.validarFormulario();
  }

  private validarFormulario(): void {
    const opcoesFormulario: AbstractControlOptions = {
      validators: ValidadorCampos.DeveSerIgual('senha', 'confirmarSenha')
    };

    this.formulario = this.formBuilder.group({
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      usuario: ['', Validators.required],
      senha: ['', [Validators.required, Validators.minLength(6)]],
      confirmarSenha: ['', Validators.required]
    }, opcoesFormulario);
  }
}
