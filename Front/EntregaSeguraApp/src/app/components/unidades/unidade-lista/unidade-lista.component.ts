import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Unidade } from 'src/app/models/unidade';
import { ExclusaoDialogComponent } from 'src/app/shared/components/exclusao-dialog/exclusao-dialog.component';
import { UnidadeService } from 'src/app/services/unidade/unidade.service';
import { CondominioService } from 'src/app/services/condominio/condominio.service';
import { Condominio } from 'src/app/models/condominio';
import { MatSelectChange } from '@angular/material/select';

@Component({
  selector: 'app-unidade-lista',
  templateUrl: './unidade-lista.component.html',
  styleUrls: ['./unidade-lista.component.scss']
})
export class UnidadeListaComponent implements OnInit, OnDestroy {
  public titulo: string = 'Lista de Unidades';
  public colunasExibidas: string[] = ['id', 'bloco', 'andar', 'numero', 'condominioId', 'nomeCondominio', 'actions'];
  private listaUnidades: Unidade[] = [];
  public dataSource = new MatTableDataSource<Unidade>(this.listaUnidades);
  public condominios: Condominio[] = [];
  public condominioSelecionado: number | null = null;
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Unidade>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private router: Router,
    private unidadeService: UnidadeService,
    private condominioService: CondominioService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.obterLista();
    this.obterCondominios();
    this.dataSource.paginator = this.paginator;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public excluirUnidade(id: number) {
    const dialogRef = this.dialog.open(ExclusaoDialogComponent);
    dialogRef.afterClosed().subscribe(confirmacaoExclusao => {
      if (confirmacaoExclusao) {
        this.unidadeService.excluir(id).subscribe({
          next: () => {
            this.toastr.success('Unidade excluída com sucesso', 'Sucesso!');
            this.obterLista();
          },
          error: (error: any) => {
            this.exibirErros(error);
          }
        });
      }
    });
  }

  public editarUnidade(id: number): void {
    this.router.navigate(['unidades/detalhe', id]);
  }

  public aplicarFiltro(evento: Event) {
    const filtro = (evento.target as HTMLInputElement).value;
    this.dataSource.filter = filtro.trim().toLowerCase();
  }

  public aplicarFiltroPorCondominio(event: MatSelectChange) {
    const condominioId = event.value;
    if (condominioId === null) {
      this.dataSource.filter = '';
    } else {
      this.dataSource.filterPredicate = (data: Unidade, filter: string) => data.condominioId === Number(filter);
      this.dataSource.filter = String(condominioId);
    }
  }

  private obterCondominios() {
    this.condominioService.obterTodos().subscribe({
      next: (condominios: Condominio[]) => {
        this.condominios = condominios;
      },
      error: (error: any) => {
        this.exibirErros(error);
      }
    });
  }

  private obterLista() {
    this.unidadeService.obterTodasUnidadesComCondominio().pipe(takeUntil(this.destroy$)).subscribe({
      next: (unidades: Unidade[]) => {
        this.listaUnidades = unidades;
        this.dataSource = new MatTableDataSource<Unidade>(this.listaUnidades);
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
      this.toastr.error(erro.message || 'Erro ao excluir item', 'Erro!');
    }
  }
}