import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  @Input() titulo = '';
  @Input() classeIcone = 'fa fa-building';
  @Input() subtitulo = 'Sistema de Gerenciamento de Entregas em Condomínios';
  @Input() botaoListar = false;

  constructor() { }

  ngOnInit(): void {
  }

}
