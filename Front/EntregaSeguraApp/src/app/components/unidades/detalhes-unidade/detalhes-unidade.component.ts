import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Condominio } from '@app/models/condominio.model';
import { Unidade } from '@app/models/unidade.model';
import { CondominioService } from '@app/services/condominio.service';
import { UnidadeService } from '@app/services/unidade.service';
import { TratamentoErrosService } from '@app/shared/services/tratamento-erros.service';

@Component({
  selector: 'app-detalhes-unidade',
  templateUrl: './detalhes-unidade.component.html',
  styleUrls: ['./detalhes-unidade.component.scss']
})
export class DetalhesUnidadeComponent implements OnInit, OnDestroy {
  public titulo: string = '';
  public novaUnidade: boolean = false;
  public mascaraTelefone: string = '(00) 0000-00009';
  public formulario: FormGroup = new FormGroup({});
  public condominios: Condominio[] = [];

  private unidadeId: number = 0;
  private unidade: Unidade = {} as Unidade;
  private destroy$ = new Subject<void>();

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly unidadeService: UnidadeService,
    private condominioService: CondominioService,
    private readonly toastr: ToastrService,
    private readonly spinner: NgxSpinnerService,
    private readonly tratamentoErrosService: TratamentoErrosService,
    private readonly dialog: MatDialog
  ) { }

  get formControl(): any {
    return this.formulario.controls;
  }

  ngOnInit(): void {
    this.definirOperacao();
    this.validarformulario();
    this.carregarUnidade();
    this.carregarCondominios();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public submeterFormulario(): void {
    this.spinner.show();

    if (this.formulario.invalid) {
      this.spinner.hide();
      return;
    }

    const unidade: Partial<Unidade> = this.formulario.getRawValue();
    let operacao: Observable<Unidade>;

    if (this.novaUnidade) {
      operacao = this.criarUnidade(unidade as Unidade);
    } else {
      unidade.id = this.unidadeId;
      operacao = this.atualizarUnidade(unidade as Unidade);
    }

    operacao.subscribe({
      next: () => {
        this.toastr.success(`Unidade ${this.unidadeId ? 'atualizada' : 'criada'} com sucesso!`, 'Sucesso');
        this.router.navigate(['/unidades']);
      },
      error: (error: any) => this.tratarErros(error),
      complete: () => this.spinner.hide()
    });
  }

  public reiniciarFormulario(event: any): void {
    event.preventDefault();
    this.formulario.reset();
    this.formulario.markAsPristine();
    this.formulario.markAsUntouched();
  }

  private definirOperacao(): void {
    this.route.paramMap.subscribe(params => {
      this.unidadeId = Number(params.get('id')) || 0;

      if (this.unidadeId == 0) {
        this.novaUnidade = true;
        this.titulo = 'Nova Unidade';
      } else {
        this.novaUnidade = false;
        this.titulo = 'Detalhes da Unidade';
      }
    });
  }

  private carregarUnidade(): void {
    if (!this.novaUnidade) {
      this.spinner.show();

      this.unidadeService.ObterUnidadePorId(this.unidadeId).subscribe({
        next: (unidade: Unidade) => {
          this.unidade = { ...unidade };
          this.formulario.patchValue(this.unidade);
          this.titulo = 'Edição: ' + this.unidade.descricaoUnidade;
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error(error.message, 'Erro!');
          console.error(error);
        },
        complete: () => this.spinner.hide()
      });
    }
  }

  private carregarCondominios() {
    this.condominioService.obterCondominios().pipe(takeUntil(this.destroy$)).subscribe({
      next: (condominios: Condominio[]) => {
        this.condominios = condominios;
      },
      error: (error: any) => {
        this.tratarErros(error);
      }
    });
  }

  private validarformulario(): void {
    this.formulario = this.formBuilder.group({
      bloco: ['', [Validators.required, Validators.min(1), Validators.max(20)]],
      andar: ['', [Validators.required, Validators.min(1), Validators.max(40)]],
      numero: ['', [Validators.required, Validators.min(1), Validators.max(10)]],
      condominioId: ['', Validators.required]
    });
  }

  private atualizarUnidade(unidade: Unidade): Observable<Unidade> {
    return this.unidadeService.atualizar(unidade.id, unidade);
  }

  private criarUnidade(unidade: Unidade): Observable<Unidade> {
    return this.unidadeService.criar(unidade);
  }

  private tratarErros(erro: any): void {
    this.spinner.hide();
    this.tratamentoErrosService.tratarErro(erro).subscribe({
      error: (mensagemErro: any) => {
        if (mensagemErro.message && typeof mensagemErro.message === 'string') {
          const mensagensErro = mensagemErro.message.split(',');
          for (const mensagem of mensagensErro) {
            this.toastr.error(mensagem.trim(), 'Erro!');
          }
        }
      }
    });
  }
}
