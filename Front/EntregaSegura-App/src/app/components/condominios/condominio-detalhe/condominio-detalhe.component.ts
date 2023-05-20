import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-condominio-detalhe',
  templateUrl: './condominio-detalhe.component.html',
  styleUrls: ['./condominio-detalhe.component.scss']
})
export class CondominioDetalheComponent implements OnInit {
  formulario: FormGroup = new FormGroup({});

  get formControl(): any {
    return this.formulario.controls;
  }

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.validacao();
  }

  public validacao(): void {
    this.formulario = this.formBuilder.group({
      quantidadeUnidades: ['', Validators.required],
      quantidadeAndares: ['', Validators.required],
      quantidadeBlocos: ['', Validators.required],
      nome: ['', Validators.required],
      cnpj: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      logradouro: ['', Validators.required],
      numero: ['', Validators.required],
      complemento: ['', Validators.required ],
      cep: ['', Validators.required],
      bairro: ['', Validators.required],
      cidade: ['', Validators.required],
      estado: ['',Validators.required]
    });
  }
}
