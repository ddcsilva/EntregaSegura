import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { Morador } from '@app/models/morador.model';
import { MoradorService } from '@app/services/morador.service';
import { ExclusaoDialogComponent } from '@app/shared/components/exclusao-dialog/exclusao-dialog.component';

@Component({
  selector: 'app-moradores',
  templateUrl: './moradores.component.html',
  styleUrls: ['./moradores.component.scss']
})
export class MoradoresComponent implements OnInit, OnDestroy {
  public titulo: string = 'Lista de Moradores';
  public moradores: Morador[] = [];
  public colunas: string[] = ['nome', 'telefone', 'ramal', 'nomeCondominio', 'descricaoUnidade', 'acoes'];
  public dataSource: MatTableDataSource<Morador> = new MatTableDataSource<Morador>();
  public filtroMorador: string = '';
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Morador>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private moradorService: MoradorService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.obterMoradores();
    this.dataSource.paginator = this.paginator;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public filtrarMoradores(): void {
    this.dataSource.filter = this.filtroMorador.trim().toLowerCase();
  }

  public editarMorador(id: number): void {
    this.router.navigate(['moradores', id]);
  }

  public excluirMorador(id: number) {
    const dialogRef = this.dialog.open(ExclusaoDialogComponent);
    dialogRef.afterClosed().subscribe(confirmacaoExclusao => {
      if (confirmacaoExclusao) {
        this.moradorService.excluir(id).subscribe({
          next: () => {
            this.toastr.success('Morador excluído com sucesso', 'Sucesso!');
            this.obterMoradores();
          },
          error: (error: any) => {
            this.exibirErros(error);
          }
        });
      }
    });
  }

  private obterMoradores(): void {
    this.moradorService.obterMoradores().pipe(takeUntil(this.destroy$)).subscribe({
      next: (response) => {
        this.moradores = response;
        this.dataSource = new MatTableDataSource<Morador>(this.moradores);
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
