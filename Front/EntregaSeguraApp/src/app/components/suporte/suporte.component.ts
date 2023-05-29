import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-suporte',
  templateUrl: './suporte.component.html',
  styleUrls: ['./suporte.component.scss']
})
export class SuporteComponent implements OnInit {

  suporteForm!: FormGroup;
  
  assuntos = [
    {value: 'tecnico', viewValue: 'Problema Técnico'},
    {value: 'financeiro', viewValue: 'Questão Financeira'},
    {value: 'geral', viewValue: 'Questão Geral'}
  ];

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.suporteForm = this.formBuilder.group({
      assunto: ['', Validators.required],
      mensagem: ['', Validators.required]
    });
  }

  enviarMensagem() {
    if (this.suporteForm.valid) {
      console.log(this.suporteForm.value);
      // Aqui você pode enviar os dados para o backend.
    }
  }
}
