import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { Funcionario } from 'src/app/models/funcionario.model';
import { FuncionarioService } from 'src/app/services/funcionario.service';
import { ExclusaoDialogComponent } from 'src/app/shared/components/exclusao-dialog/exclusao-dialog.component';

@Component({
  selector: 'app-funcionarios',
  templateUrl: './funcionarios.component.html',
  styleUrls: ['./funcionarios.component.scss']
})
export class FuncionariosComponent implements OnInit, OnDestroy {
  public funcionarios: Funcionario[] = [];
  public colunas: string[] = ['nome', 'telefone', 'cargo', 'nomeCondominio', 'acoes'];
  public dataSource: MatTableDataSource<Funcionario> = new MatTableDataSource<Funcionario>();
  public filtroFuncionario: string = '';
  public titulo: string = 'Lista de Funcionários';
  private destroy$ = new Subject<void>();

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Funcionario>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private funcionarioService: FuncionarioService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.obterFuncionarios();
    this.dataSource.paginator = this.paginator;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public filtrarFuncionarios(): void {
    this.dataSource.filter = this.filtroFuncionario.trim().toLowerCase();
  }

  public editarFuncionario(id: number): void {
    this.router.navigate(['funcionarios', id]);
  }

  public excluirFuncionario(id: number) {
    const dialogRef = this.dialog.open(ExclusaoDialogComponent);
    dialogRef.afterClosed().subscribe(confirmacaoExclusao => {
      if (confirmacaoExclusao) {
        this.funcionarioService.excluir(id).subscribe({
          next: () => {
            this.toastr.success('Funcionário excluído com sucesso', 'Sucesso!');
            this.obterFuncionarios();
          },
          error: (error: any) => {
            this.exibirErros(error);
          }
        });
      }
    });
  }

  private obterFuncionarios(): void {
    this.funcionarioService.obterFuncionarios().pipe(takeUntil(this.destroy$)).subscribe({
      next: (response) => {
        this.funcionarios = response;
        this.dataSource = new MatTableDataSource<Funcionario>(this.funcionarios);
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
