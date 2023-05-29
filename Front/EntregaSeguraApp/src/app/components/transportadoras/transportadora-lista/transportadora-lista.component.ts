// Angular imports
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';

// Model imports
import { Transportadora } from 'src/app/models/transportadora';

// Service imports
import { TransportadoraService } from 'src/app/services/transportadora/transportadora.service';

// Component imports
import { ExclusaoDialogComponent } from 'src/app/shared/components/exclusao-dialog/exclusao-dialog.component';

// Library imports
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-transportadora-lista',
  templateUrl: './transportadora-lista.component.html',
  styleUrls: ['./transportadora-lista.component.scss']
})
export class TransportadoraListaComponent implements OnInit, OnDestroy {
  public titulo: string = 'Lista de Transportadoras';
  public colunasExibidas: string[] = ['id', 'nome', 'telefone', 'cnpj', 'email', 'actions'];
  private listaTransportadoras: Transportadora[] = [];
  public dataSource = new MatTableDataSource<Transportadora>(this.listaTransportadoras);
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Transportadora>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private transportadoraService: TransportadoraService,
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

  private obterLista() {
    this.transportadoraService.obterTodos().pipe(takeUntil(this.destroy$)).subscribe({
      next: (transportadoras: Transportadora[]) => {
        this.listaTransportadoras = transportadoras;
        this.dataSource = new MatTableDataSource<Transportadora>(this.listaTransportadoras);
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
  
  public excluirTransportadora(id: number) {
    const dialogRef = this.dialog.open(ExclusaoDialogComponent);
    dialogRef.afterClosed().subscribe(confirmacaoExclusao => {
      if (confirmacaoExclusao) {
        this.transportadoraService.excluir(id.toString()).subscribe({
          next: () => {
            this.toastr.success('Transportadora excluída com sucesso', 'Sucesso!');
            this.obterLista();
          },
          error: (error: any) => {
            this.exibirErros(error);
          }
        });
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

  public aplicarFiltro(evento: Event) {
    const filtro = (evento.target as HTMLInputElement).value;
    this.dataSource.filter = filtro.trim().toLowerCase();
  }
}
