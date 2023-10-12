import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Entrega } from '@app/models/entrega.model';
import { EntregaService } from '@app/services/entrega.service';
import { UsuarioService } from '@app/services/usuario.service';
import { ConfirmacaoDialogComponent } from '@app/shared/components/confirmacao-dialog/confirmacao-dialog.component';
import { ExclusaoDialogComponent } from '@app/shared/components/exclusao-dialog/exclusao-dialog.component';
import { InformacoesConfirmacaoDialog } from '@app/shared/models/InformacoesConfirmacaoDialog.model';
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
  public colunas: string[] = [];
  public dataSource: MatTableDataSource<Entrega> = new MatTableDataSource<Entrega>();
  public filtroEntrega: string = '';
  private destroy$ = new Subject<void>();
  public emailUsuario: string = '';
  public perfilUsuario: string = '';

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatTable, { static: true }) table!: MatTable<Entrega>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private entregaService: EntregaService,
    private usuarioService: UsuarioService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    if (this.perfilUsuario !== 'Morador') {
      this.colunas = ['dataRecebimento', 'nomeMorador', 'descricaoUnidade', 'status', 'acoes'];
    } else {
      this.colunas = ['dataRecebimento', 'nomeMorador', 'descricaoUnidade', 'status'];
    }

    this.usuarioService.obterEmailDaClaim().subscribe(email => {
      this.emailUsuario = email;
    });

    this.usuarioService.obterPerfilDaClaim().subscribe(perfil => {
      this.perfilUsuario = perfil;
    });

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

  public confirmarRetirada(id: number) {
    let entrega = this.entregas.find(e => e.id === id);

    if (entrega) {
      const dialogConfig = {
        data: {
          titulo: 'Confirmação de Retirada de Entrega',
          mensagem: `Tem certeza de que deseja confirmar a retirada da entrega pelo morador ${entrega.nomeMorador}?`,
          informacaoAdicional: `A entrega está endereçada para a unidade ${entrega.descricaoUnidade}. Ao confirmar, o status da entrega será alterado para "Retirada" e não poderá ser revertido.`,
          textoBotaoCancelar: 'Cancelar',
          textoBotaoConfirmar: 'Confirmar',
        } as InformacoesConfirmacaoDialog
      }

      const dialogRef = this.dialog.open(ConfirmacaoDialogComponent, dialogConfig);

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.entregaService.confirmarRetirada(id).subscribe({
            next: () => {
              this.toastr.success('Retirada da entrega pelo morador confirmada com sucesso!', 'Sucesso!');
              this.obterEntregas();
            },
            error: (error: any) => {
              this.exibirErros(error);
            }
          });
        }
      });
    }
  }

  public notificarEntrega(id: number) {
    let entrega = this.entregas.find(e => e.id === id);

    if (entrega) {
      const dialogConfig = {
        data: {
          titulo: 'Confirmação de Envio de Notificação',
          mensagem: `Tem certeza de que deseja enviar uma notificação de entrega para o morador ${entrega.nomeMorador}?`,
          informacaoAdicional: `Será enviada uma notificação para o e-mail ${entrega.emailMorador} para informar que a entrega está disponível para retirada.`,
          textoBotaoCancelar: 'Cancelar',
          textoBotaoConfirmar: 'Confirmar',
        } as InformacoesConfirmacaoDialog
      }

      const dialogRef = this.dialog.open(ConfirmacaoDialogComponent, dialogConfig);

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.entregaService.notificarEntrega(id).subscribe({
            next: () => {
              this.toastr.success('Notificação enviada para o morador com sucesso!', 'Sucesso!');
              this.obterEntregas();
            },
            error: (error: any) => {
              this.exibirErros(error);
            }
          });
        }
      });
    }
  }

  private obterEntregas(): void {
    this.entregaService.obterEntregas(this.emailUsuario, this.perfilUsuario).pipe(takeUntil(this.destroy$)).subscribe({
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
      this.toastr.error(erro, 'Houve um erro!');
    } else if (erro instanceof Array) {
      erro.forEach(mensagemErro => this.toastr.error(mensagemErro, 'Houve um erro!'));
    } else {
      this.toastr.error(erro.message || 'Erro ao excluir!', 'Houve um erro!');
    }
  }
}
