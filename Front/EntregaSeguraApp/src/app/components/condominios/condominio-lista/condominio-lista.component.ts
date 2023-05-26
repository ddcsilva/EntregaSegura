import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-condominio-lista',
  templateUrl: './condominio-lista.component.html',
  styleUrls: ['./condominio-lista.component.scss']
})
export class CondominioListaComponent implements OnInit {

  displayedColumns: string[] = ['key', 'name', 'phoneNumber', 'bairro', 'cidade', 'estado', 'actions'];
  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  sort!: MatSort;

  constructor() {
    // Aqui vamos criar alguns dados falsos para preencher a tabela
    const condominios = [
      { key: 1, name: 'Condominio A', phoneNumber: '123456789', bairro: 'Bairro A', cidade: 'Cidade A', estado: 'Estado A' },
      { key: 2, name: 'Condominio B', phoneNumber: '987654321', bairro: 'Bairro B', cidade: 'Cidade B', estado: 'Estado B' },
      // Continue adicionando os condomínios que você deseja exibir
    ];

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(condominios);
  }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  deleteItem(key: number) {
    console.log('Item deleted: ' + key);
  }

}
