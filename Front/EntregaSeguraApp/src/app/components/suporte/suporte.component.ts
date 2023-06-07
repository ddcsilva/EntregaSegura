import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-suporte',
  templateUrl: './suporte.component.html',
  styleUrls: ['./suporte.component.scss']
})
export class SuporteComponent implements OnInit {
  suporteForm: FormGroup;
  isLoading: boolean = false;
  assuntos = [
    {value: 'bug', viewValue: 'Reportar um bug'},
    {value: 'questao', viewValue: 'Questão sobre uso'},
    {value: 'pedido', viewValue: 'Pedido de nova funcionalidade'},
  ];

  constructor(private fb: FormBuilder) {
    this.suporteForm = this.fb.group({
      assunto: ['', Validators.required],
      mensagem: ['', Validators.required],
      arquivos: [''],
      enviarCopia: ['']
    });
  }

  ngOnInit() {}

  enviarMensagem() {
    if (this.suporteForm.valid) {
      this.isLoading = true;
      // Enviar dados do formulário
    }
  }
}
