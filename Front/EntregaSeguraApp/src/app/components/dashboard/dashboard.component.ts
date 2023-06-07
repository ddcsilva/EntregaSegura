import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  totalEntregas = 1200;
  entregasPendentes = 30;
  unidadesOcupadas = 150;
  entregasConcluidas = 1050;

  public lineChartData: ChartConfiguration['data'] = {
    datasets: [
      {
        data: [ 65, 59, 80, 81, 56, 55, 40 ],
        label: 'Entregas pendentes',
        backgroundColor: 'rgba(148,159,177,0.2)',
        borderColor: '#673AB7',
        pointBackgroundColor: '#673AB7',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: '#673AB7',
        fill: 'origin',
      },
      {
        data: [ 28, 48, 40, 19, 86, 27, 90 ],
        label: 'Entregas concluídas',
        backgroundColor: 'rgba(77,83,96,0.2)',
        borderColor: '#E91E63',
        pointBackgroundColor: '#E91E63',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: '#E91E63',
        fill: 'origin',
      }
    ],
    labels: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho']
  };

  public lineChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    elements: {
      line: {
        tension: 0.5
      }
    },
    scales: {
      y: {
        position: 'left',
      }
    },
    plugins: {
      legend: { display: true }
    }
  };

  public lineChartType: ChartType = 'line';

  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

  constructor() { }

  ngOnInit(): void {
  }

}
