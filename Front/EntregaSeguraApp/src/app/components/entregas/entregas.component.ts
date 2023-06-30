import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Entrega } from '@app/models/entrega.model';
import { EntregaService } from '@app/services/entrega.service';
import { ExclusaoDialogComponent } from '@app/shared/components/exclusao-dialog/exclusao-dialog.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-entregas',
  templateUrl: './entregas.component.html',
  styleUrls: ['./entregas.component.scss']
})
export class EntregasComponent implements OnInit, OnDestroy {
  public titulo: string = 'Lista de Entregas';
  public entregas: Entrega[] = [];
  public colunas: string[] = ['dataRecebimento', 'nomeMorador', 'descricaoUnidade', 'status', 'acoes'];
  public dataSource: MatTableDataSource<Entrega> = new MatTableDataSource<Entrega>();
  public filtroEntrega: string = '';
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Entrega>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private entregaService: EntregaService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.obterEntregas();
    this.dataSource.paginator = this.paginator;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public filtrarEntregas(): void {
    this.dataSource.filter = this.filtroEntrega.trim().toLowerCase();
  }

  public editarEntrega(id: number): void {
    this.router.navigate(['entregas', id]);
  }

  public excluirEntrega(id: number) {
    const dialogRef = this.dialog.open(ExclusaoDialogComponent);
    dialogRef.afterClosed().subscribe(confirmacaoExclusao => {
      if (confirmacaoExclusao) {
        this.entregaService.excluir(id).subscribe({
          next: () => {
            this.toastr.success('Entrega excluída com sucesso', 'Sucesso!');
            this.obterEntregas();
          },
          error: (error: any) => {
            this.exibirErros(error);
          }
        });
      }
    });
  }

  private obterEntregas(): void {
    this.entregaService.obterEntregas().pipe(takeUntil(this.destroy$)).subscribe({
      next: (response) => {
        this.entregas = response;
        this.dataSource = new MatTableDataSource<Entrega>(this.entregas);
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
