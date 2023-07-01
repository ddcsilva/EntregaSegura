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
import { Transportadora } from '@app/models/transportadora.model';
import { TransportadoraService } from '@app/services/transportadora.service';
import { ExclusaoDialogComponent } from '@app/shared/components/exclusao-dialog/exclusao-dialog.component';

@Component({
  selector: 'app-transportadoras',
  templateUrl: './transportadoras.component.html',
  styleUrls: ['./transportadoras.component.scss']
})
export class TransportadorasComponent implements OnInit, OnDestroy {
  public titulo: string = 'Lista de Transportadoras';
  public transportadoras: Transportadora[] = [];
  public colunas: string[] = ['nome', 'telefone', 'cnpj', 'email', 'acoes'];
  public dataSource: MatTableDataSource<Transportadora> = new MatTableDataSource<Transportadora>();
  public filtroTransportadora: string = '';
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Condominio>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private transportadoraService: TransportadoraService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.obterTransportadoras();
    this.dataSource.paginator = this.paginator;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public filtrarTransportadoras(): void {
    this.dataSource.filter = this.filtroTransportadora.trim().toLowerCase();
  }

  public editarTransportadora(id: number): void {
    this.router.navigate(['transportadoras', id]);
  }

  public excluirTransportadora(id: number) {
    const dialogRef = this.dialog.open(ExclusaoDialogComponent);
    dialogRef.afterClosed().subscribe(confirmacaoExclusao => {
      if (confirmacaoExclusao) {
        this.transportadoraService.excluir(id).subscribe({
          next: () => {
            this.toastr.success('Transportadora excluída com sucesso', 'Sucesso!');
            this.obterTransportadoras();
          },
          error: (error: any) => {
            this.exibirErros(error);
          }
        });
      }
    });
  }

  private obterTransportadoras(): void {
    this.transportadoraService.obterTransportadoras().pipe(takeUntil(this.destroy$)).subscribe({
      next: (response) => {
        this.transportadoras = response;
        this.dataSource = new MatTableDataSource<Transportadora>(this.transportadoras);
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
