import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Condominio } from 'src/app/models/condominio.model';
import { Morador } from 'src/app/models/morador.model';
import { Unidade } from 'src/app/models/unidade.model';
import { UnidadesEmMassa } from 'src/app/models/unidades-em-massa.model';
import { CondominioService } from 'src/app/services/condominio.service';
import { MoradorService } from 'src/app/services/morador.service';
import { UnidadeService } from 'src/app/services/unidade.service';
import { ConfirmacaoDialogComponent } from 'src/app/shared/components/confirmacao-dialog/confirmacao-dialog.component';
import { ValidadorCampos } from 'src/app/shared/helpers/validador-campos';
import { InformacoesConfirmacaoDialog } from 'src/app/shared/models/InformacoesConfirmacaoDialog.model';
import { CepService } from 'src/app/shared/services/cep.service';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros.service';

@Component({
  selector: 'app-detalhes-morador',
  templateUrl: './detalhes-morador.component.html',
  styleUrls: ['./detalhes-morador.component.scss']
})
export class DetalhesMoradorComponent implements OnInit, OnDestroy {
  public novoMorador: boolean = false;
  public titulo: string = '';
  public mascaraTelefone: string = '(00) 0000-00009';
  public formulario: FormGroup = new FormGroup({});
  public condominios: Condominio[] = [];
  public unidades: Unidade[] = [];

  private moradorId: number = 0;
  private morador: Morador = {} as Morador;
  private destroy$ = new Subject<void>();

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly moradorService: MoradorService,
    private readonly condominioService: CondominioService,
    private readonly unidadeService: UnidadeService,
    private readonly toastr: ToastrService,
    private readonly spinner: NgxSpinnerService,
    private readonly tratamentoErrosService: TratamentoErrosService
  ) { }

  get formControl(): any {
    return this.formulario.controls;
  }

  ngOnInit(): void {
    this.definirOperacao();
    this.validarformulario();
    this.carregarMorador();
    this.carregarCondominios();

    this.formControl.telefone.valueChanges.subscribe((value: any) => {
      this.atualizarMascaraTelefone(value);
    });

    this.formControl.condominioId.valueChanges.subscribe((condominioId: any) => {
      if (condominioId) {
        this.carregarUnidades(condominioId);
      }
    });
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

    const morador: Partial<Morador> = this.formulario.getRawValue();
    let operacao: Observable<Morador>;

    if (this.novoMorador) {
      operacao = this.criarMorador(morador as Morador);
    } else {
      morador.id = this.moradorId;
      operacao = this.atualizarMorador(morador as Morador);
    }

    operacao.subscribe({
      next: () => {
        this.toastr.success(`Morador ${this.moradorId ? 'atualizado' : 'criado'} com sucesso!`, 'Sucesso');
        this.router.navigate(['/moradores']);
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
      this.moradorId = Number(params.get('id')) || 0;

      if (this.moradorId == 0) {
        this.novoMorador = true;
        this.titulo = 'Novo Morador';
      } else {
        this.novoMorador = false;
        this.titulo = 'Detalhes do Morador';
      }
    });
  }

  private carregarMorador(): void {
    if (!this.novoMorador) {
      this.spinner.show();

      this.moradorService.obterMoradorPorId(this.moradorId).subscribe({
        next: (morador: Morador) => {
          this.morador = { ...morador };
          this.formulario.patchValue(this.morador);
          this.titulo = 'Edição: ' + this.morador.nome;
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

  private carregarUnidades(condominioId: number) {
    this.unidadeService.ObterUnidadesPorCondominio(condominioId).pipe(takeUntil(this.destroy$)).subscribe({
      next: (unidades: Unidade[]) => {
        this.unidades = unidades;
        this.formControl.unidadeId.enable();
      },
      error: (error: any) => {
        this.tratarErros(error);
      }
    });
  }

  private validarformulario(): void {
    this.formulario = this.formBuilder.group({
      condominioId: ['', Validators.required],
      unidadeId: [{ value: '', disabled: true }, Validators.required],
      nome: ['', Validators.required],
      cpf: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      ramal: ['']
    });
  }

  private atualizarMorador(morador: Morador): Observable<Morador> {
    return this.moradorService.atualizar(morador.id, morador);
  }

  private criarMorador(morador: Morador): Observable<Morador> {
    return this.moradorService.criar(morador);
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

  private atualizarMascaraTelefone(value: string): void {
    if (value) {
      const numbers = value.replace(/\D/g, '');
      this.mascaraTelefone = numbers.length > 10 ? '(00) 00000-0000' : '(00) 0000-00009';
    } else {
      this.mascaraTelefone = '(00) 0000-00009';
    }
  }
}
