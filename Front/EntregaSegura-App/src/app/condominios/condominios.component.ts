import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-condominios',
  templateUrl: './condominios.component.html',
  styleUrls: ['./condominios.component.scss']
})
export class CondominiosComponent implements OnInit {

  public condominios: any = [];
  public condominiosFiltrados: any = [];

  private _filtro: string = '';

  public get filtro(): string {
    return this._filtro;
  }
  public set filtro(value: string) {
    this._filtro = value;
    this.condominiosFiltrados = this.filtro ? this.filtrarCondominios(this.filtro) : this.condominios;
  }

  public filtrarCondominios(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.condominios.filter(
      (condominio: { cnpj: string; nome: string; cidade: string }) =>
        condominio.cnpj.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        condominio.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        condominio.cidade.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getCondominios();
  }

  public getCondominios(): void {
    this.http.get('https://localhost:5001/api/condominios').subscribe({
      next: response => {
        this.condominios = response;
        this.condominiosFiltrados = this.condominios;
      },
      error: error => {
        console.log(error);
      }
    });
  }
}
