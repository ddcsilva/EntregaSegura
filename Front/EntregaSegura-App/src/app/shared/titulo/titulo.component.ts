import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

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
  @Input() rotaBotaoListar = '';

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  navegarParaRotaBotaoListar(): void {
    this.router.navigate([this.rotaBotaoListar]);
  }

}
