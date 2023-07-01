import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { Condominio } from '@app/models/condominio.model';
import { CondominioService } from '@app/services/condominio.service';
import { ExclusaoDialogComponent } from '@app/shared/components/exclusao-dialog/exclusao-dialog.component';

@Component({
  selector: 'app-condominios',
  templateUrl: './condominios.component.html',
  styleUrls: ['./condominios.component.scss']
})
export class CondominiosComponent implements OnInit, OnDestroy {
  public titulo: string = 'Lista de Condomínios';
  public condominios: Condominio[] = [];
  public colunas: string[] = ['nome', 'telefone', 'bairro', 'cidade', 'estado', 'acoes'];
  public dataSource: MatTableDataSource<Condominio> = new MatTableDataSource<Condominio>();
  public filtroCondominio: string = '';
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Condominio>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private condominioService: CondominioService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.obterCondominios();
    this.dataSource.paginator = this.paginator;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public filtrarCondominios(): void {
    this.dataSource.filter = this.filtroCondominio.trim().toLowerCase();
  }

  public editarCondominio(id: number): void {
    this.router.navigate(['condominios', id]);
  }

  public excluirCondominio(id: number) {
    const dialogRef = this.dialog.open(ExclusaoDialogComponent);
    dialogRef.afterClosed().subscribe(confirmacaoExclusao => {
      if (confirmacaoExclusao) {
        this.condominioService.excluir(id).subscribe({
          next: () => {
            this.toastr.success('Condomínio excluído com sucesso', 'Sucesso!');
            this.obterCondominios();
          },
          error: (error: any) => {
            this.exibirErros(error);
          }
        });
      }
    });
  }

  private obterCondominios(): void {
    this.condominioService.obterCondominios().pipe(takeUntil(this.destroy$)).subscribe({
      next: (response) => {
        this.condominios = response;
        this.dataSource = new MatTableDataSource<Condominio>(this.condominios);
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
      this.toastr.error(erro, 'Houve um erro!');
    } else if (erro instanceof Array) {
      erro.forEach(mensagemErro => this.toastr.error(mensagemErro, 'Houve um erro!'));
    } else {
      this.toastr.error(erro.message || 'Erro ao excluir', 'Houve um erro!');
    }
  }
}
