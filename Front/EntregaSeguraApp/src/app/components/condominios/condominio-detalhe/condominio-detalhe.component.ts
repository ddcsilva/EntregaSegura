import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-condominio-detalhe',
  templateUrl: './condominio-detalhe.component.html',
  styleUrls: ['./condominio-detalhe.component.scss']
})
export class CondominioDetalheComponent implements OnInit {
  public titulo: string = 'Novo Condomínio';

  condominioForm = this.fb.group({
    nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    cnpj: ['', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(14)]],
    telefone: ['', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.minLength(10), Validators.maxLength(11)]],
    email: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    logradouro: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    numero: ['', [Validators.required, Validators.maxLength(10)]],
    complemento: ['', [Validators.maxLength(50)]],
    cep: ['', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(8)]],
    bairro: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
    cidade: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
    estado: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(2)]],
    quantidadeUnidades: ['', [Validators.required, Validators.min(1), Validators.max(1000)]],
    quantidadeAndares: ['', [Validators.required, Validators.min(1), Validators.max(50)]],
    quantidadeBlocos: ['', [Validators.required, Validators.min(1), Validators.max(50)]],
  });

  constructor(private fb: FormBuilder, private route: ActivatedRoute) { }

  ngOnInit() {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.titulo = 'Detalhe do Condomínio: ' + id;
    }
  }

  onSubmit() {
    if (this.condominioForm.valid) {
      // Faça algo com os dados do formulário aqui
    }
  }

  get f() {
    return this.condominioForm.controls;
  }

  buscarCep() {
    // implemente a funcionalidade aqui
  }

}
