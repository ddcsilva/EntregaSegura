import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-condominios',
  templateUrl: './condominios.component.html',
  styleUrls: ['./condominios.component.scss']
})
export class CondominiosComponent implements OnInit {
  displayedColumns: string[] = ['column1', 'column2', 'column3', 'editar'];
  dataSource: any[] = [
    { column1: 'Valor 1', column2: 'Valor 2', column3: 'Valor 3' },
    { column1: 'Valor 4', column2: 'Valor 5', column3: 'Valor 6' },
    { column1: 'Valor 7', column2: 'Valor 8', column3: 'Valor 9' }
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
