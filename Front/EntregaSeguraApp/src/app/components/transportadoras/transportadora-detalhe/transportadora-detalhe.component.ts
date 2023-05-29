import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-transportadora-detalhe',
  templateUrl: './transportadora-detalhe.component.html',
  styleUrls: ['./transportadora-detalhe.component.scss']
})
export class TransportadoraDetalheComponent implements OnInit {
  public titulo: string = 'Nova Transportadora';

  transportadoraForm = this.fb.group({
    nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    cnpj: ['', [Validators.pattern('^[0-9]*$'), Validators.maxLength(14)]],
    telefone: ['', [Validators.pattern('^[0-9]*$'), Validators.minLength(10), Validators.maxLength(11)]],
    email: ['', [Validators.minLength(2), Validators.maxLength(100)]],
  });

  constructor(private fb: FormBuilder, private route: ActivatedRoute) { }

  ngOnInit() {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.titulo = 'Detalhe da Transportadora: ' + id;
    }
  }

  onSubmit() {
    if (this.transportadoraForm.valid) {
      // Faça algo com os dados do formulário aqui
    }
  }

  get f() {
    return this.transportadoraForm.controls;
  }
}
