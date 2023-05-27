import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  totalEntregas = 1200;
  entregasPendentes = 30;
  unidadesOcupadas = 150;

  dadosTransportadora = [
    ['Transportadora', 'Entregas'],
    ['Transportadora 1', 400],
    ['Transportadora 2', 350],
    ['Transportadora 3', 300],
  ];

  dadosEntregasPorMes = [
    ['Mês', 'Entregas'],
    ['Janeiro', 100],
    ['Fevereiro', 120],
    ['Março', 130],
    ['Abril', 90],
    ['Maio', 115],
    ['Junho', 95],
    ['Julho', 130],
    ['Agosto', 120],
    ['Setembro', 110],
    ['Outubro', 100],
    ['Novembro', 120],
    ['Dezembro', 140]
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
