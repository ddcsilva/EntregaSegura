import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSelectChange } from '@angular/material/select';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { Condominio } from 'src/app/models/condominio.model';
import { Unidade } from 'src/app/models/unidade.model';
import { CondominioService } from 'src/app/services/condominio.service';
import { UnidadeService } from 'src/app/services/unidade.service';
import { ExclusaoDialogComponent } from 'src/app/shared/components/exclusao-dialog/exclusao-dialog.component';

@Component({
  selector: 'app-unidades',
  templateUrl: './unidades.component.html',
  styleUrls: ['./unidades.component.scss']
})
export class UnidadesComponent implements OnInit, OnDestroy {
  public unidades: Unidade[] = [];
  public condominios: Condominio[] = [];
  public condominioSelecionado: number = 0;
  public colunas: string[] = ['bloco', 'andar', 'numero', 'nomeCondominio', 'acoes'];
  public dataSource: MatTableDataSource<Unidade> = new MatTableDataSource<Unidade>();
  public filtroUnidade: string = '';
  public titulo: string = 'Lista de Unidades';
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Unidade>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private unidadeService: UnidadeService,
    private condominioService: CondominioService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.obterUnidades();
    this.obterCondominios();
    this.dataSource.paginator = this.paginator;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public filtrarUnidades(): void {
    this.dataSource.filter = this.filtroUnidade.trim().toLowerCase();
  }

  public editarUnidade(id: number): void {
    this.router.navigate(['unidades', id]);
  }

  public excluirUnidade(id: number) {
    const dialogRef = this.dialog.open(ExclusaoDialogComponent);
    dialogRef.afterClosed().subscribe(confirmacaoExclusao => {
      if (confirmacaoExclusao) {
        this.unidadeService.excluir(id).subscribe({
          next: () => {
            this.toastr.success('Unidade excluída com sucesso', 'Sucesso!');
            this.obterUnidades();
          },
          error: (error: any) => {
            this.exibirErros(error);
          }
        });
      }
    });
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

  private obterUnidades(): void {
    this.unidadeService.obterUnidades().pipe(takeUntil(this.destroy$)).subscribe({
      next: (response) => {
        this.unidades = response;
        this.dataSource = new MatTableDataSource<Unidade>(this.unidades);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.table.renderRows();
        this.spinner.hide();
      },
      error: (error) => {
        this.exibirErros(error);
        this.spinner.hide();
      }
    });
  }

  private obterCondominios() {
    this.condominioService.obterCondominios().pipe(takeUntil(this.destroy$)).subscribe({
      next: (condominios: Condominio[]) => {
        this.condominios = condominios;
      },
      error: (error: any) => {
        this.exibirErros(error);
      }
    });
  }

  private exibirErros(erro: any) {
    if (typeof erro === 'string') {
      this.toastr.error(erro, 'Erro!');
    } else if (erro instanceof Array) {
      erro.forEach(mensagemErro => this.toastr.error(mensagemErro, 'Erro!'));
    } else {
      this.toastr.error(erro.message || 'Erro ao excluir', 'Erro!');
    }
  }
}
