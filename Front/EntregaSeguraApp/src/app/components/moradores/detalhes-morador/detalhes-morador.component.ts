import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Condominio } from '@app/models/condominio.model';
import { Morador } from '@app/models/morador.model';
import { Unidade } from '@app/models/unidade.model';
import { CondominioService } from '@app/services/condominio.service';
import { MoradorService } from '@app/services/morador.service';
import { UnidadeService } from '@app/services/unidade.service';
import { TratamentoErrosService } from '@app/shared/services/tratamento-erros.service';
import { ValidadorCampos } from '@app/shared/helpers/validador-campos';

@Component({
  selector: 'app-detalhes-morador',
  templateUrl: './detalhes-morador.component.html',
  styleUrls: ['./detalhes-morador.component.scss']
})
export class DetalhesMoradorComponent implements OnInit, OnDestroy {
  public titulo: string = '';
  public novoMorador: boolean = false;
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
    this.carregarCondominios();
    this.carregarMorador();
    this.validarformulario();

    this.formulario.get('pessoa')?.get('telefone')?.valueChanges.subscribe((value: any) => {
      this.atualizarMascaraTelefone(value);
    });

    this.formulario.get('condominioId')?.valueChanges.subscribe((condominioId: any) => {
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
      operacao = this.atualizarMorador(morador as Morador);
    }

    operacao.subscribe({
      next: () => {
        this.toastr.success(`Morador ${this.moradorId ? 'atualizado' : 'criado'} com sucesso!`, 'Sucesso!');
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
          this.titulo = 'Edição: ' + this.morador.pessoa.nome;
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error(error.message, 'Houve um erro!');
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
      id: [0],
      condominioId: ['', Validators.required],
      unidadeId: [{ value: '', disabled: true }, Validators.required],
      pessoa: this.formBuilder.group({
        id: [0],
        nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
        cpf: ['', [Validators.required, ValidadorCampos.ValidaCPF]],
        telefone: ['', [Validators.required, Validators.minLength(10)]],
        email: ['', [Validators.required, Validators.email]]
      }),
      ramal: ['', [Validators.required, Validators.pattern("^[1-9][0-9]*$")]]
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
            this.toastr.error(mensagem.trim(), 'Houve um erro!');
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
