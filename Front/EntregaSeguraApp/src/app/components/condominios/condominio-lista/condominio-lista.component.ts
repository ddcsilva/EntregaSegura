// Angular imports
import { Router } from '@angular/router';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';

// Model imports
import { Condominio } from 'src/app/models/condominio';

// Service imports
import { CondominioService } from 'src/app/services/condominio/condominio.service';

// Component imports
import { ExclusaoDialogComponent } from 'src/app/shared/components/exclusao-dialog/exclusao-dialog.component';

// Library imports
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-condominio-lista',
  templateUrl: './condominio-lista.component.html',
  styleUrls: ['./condominio-lista.component.scss']
})
export class CondominioListaComponent implements OnInit, OnDestroy {
  public titulo: string = 'Lista de Condomínios';
  public colunasExibidas: string[] = ['id', 'nome', 'telefone', 'bairro', 'cidade', 'estado', 'actions'];
  private listaCondominios: Condominio[] = [];
  public dataSource = new MatTableDataSource<Condominio>(this.listaCondominios);
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Condominio>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private router: Router,
    private condominioService: CondominioService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.obterLista();
    this.dataSource.paginator = this.paginator;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public excluirCondominio(id: number) {
    const dialogRef = this.dialog.open(ExclusaoDialogComponent);
    dialogRef.afterClosed().subscribe(confirmacaoExclusao => {
      if (confirmacaoExclusao) {
        this.condominioService.excluir(id.toString()).subscribe({
          next: () => {
            this.toastr.success('Condomínio excluído com sucesso', 'Sucesso!');
            this.obterLista();
          },
          error: (error: any) => {
            this.exibirErros(error);
          }
        });
      }
    });
  }

  public editarCondominio(id: number): void {
    this.router.navigate(['condominios/detalhe', id]);
  }

  public aplicarFiltro(evento: Event) {
    const filtro = (evento.target as HTMLInputElement).value;
    this.dataSource.filter = filtro.trim().toLowerCase();
  }

  private obterLista() {
    this.condominioService.obterTodos().pipe(takeUntil(this.destroy$)).subscribe({
      next: (condominios: Condominio[]) => {
        this.listaCondominios = condominios;
        this.dataSource = new MatTableDataSource<Condominio>(this.listaCondominios);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.table.renderRows();
        this.spinner.hide();
      },
      error: (error: any) => {
        this.exibirErros(error);
        this.spinner.hide();
      }
    });
  }

  private exibirErros(erro: any) {
    if (erro instanceof Array) {
      erro.forEach(mensagemErro => this.toastr.error(mensagemErro, 'Erro!'));
    } else {
      this.toastr.error(erro.message || 'Erro ao excluir', 'Erro!');
    }
  }
}
