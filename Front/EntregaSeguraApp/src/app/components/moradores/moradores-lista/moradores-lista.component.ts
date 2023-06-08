import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTable, MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from "ngx-toastr";
import { Subject, takeUntil } from "rxjs";
import { Morador } from "src/app/models/morador";
import { MoradorService } from "src/app/services/morador/morador.service";

@Component({
  selector: 'app-moradores-lista',
  templateUrl: './moradores-lista.component.html',
  styleUrls: ['./moradores-lista.component.scss']
})
export class MoradoresListaComponent implements OnInit, OnDestroy, AfterViewInit {
  public titulo: string = 'Lista de Moradores';
  public colunasExibidas: string[] = ['id', 'nome', 'email', 'telefone', 'ramal', 'nomeCondominio', 'blocoAndarUnidade', 'actions'];
  private listaMoradores: Morador[] = [];
  public dataSource = new MatTableDataSource<Morador>(this.listaMoradores);
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Morador>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private router: Router,
    private moradorService: MoradorService,
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

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.renderRows();
  }

  public aplicarFiltro(evento: Event) {
    const filtro = (evento.target as HTMLInputElement).value;
    this.dataSource.filter = filtro.trim().toLowerCase();
  }

  private obterLista() {
    this.moradorService.obterTodos().pipe(takeUntil(this.destroy$)).subscribe({
      next: (moradores: Morador[]) => {
        this.listaMoradores = moradores;
        this.dataSource = new MatTableDataSource<Morador>(this.listaMoradores);
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
