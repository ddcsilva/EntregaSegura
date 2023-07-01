import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Entrega } from '@app/models/entrega.model';
import { StatusEntrega } from '@app/models/enums/status-entrega.enum';
import { Morador } from '@app/models/morador.model';
import { Transportadora } from '@app/models/transportadora.model';
import { EntregaService } from '@app/services/entrega.service';
import { MoradorService } from '@app/services/morador.service';
import { TransportadoraService } from '@app/services/transportadora.service';
import { TratamentoErrosService } from '@app/shared/services/tratamento-erros.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-detalhes-entrega',
  templateUrl: './detalhes-entrega.component.html',
  styleUrls: ['./detalhes-entrega.component.scss']
})
export class DetalhesEntregaComponent implements OnInit {
  public titulo: string = '';
  public novaEntrega: boolean = false;
  public formulario: FormGroup = new FormGroup({});
  public moradores: Morador[] = [];
  public transportadoras: Transportadora[] = [];
  public todosStatus = Object.values(StatusEntrega);

  private entregaId: number = 0;
  private entrega: Entrega = {} as Entrega;
  private destroy$ = new Subject<void>();

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly entregaService: EntregaService,
    private readonly moradorService: MoradorService,
    private readonly transportadoraService: TransportadoraService,
    private readonly toastr: ToastrService,
    private readonly spinner: NgxSpinnerService,
    private readonly tratamentoErrosService: TratamentoErrosService
  ) { }

  get formControl(): any {
    return this.formulario.controls;
  }

  ngOnInit(): void {
    this.definirOperacao();
    this.carregarMoradores();
    this.carregarTransportadoras();
    this.carregarEntrega();
    this.validarformulario();
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

    const entrega: Partial<Entrega> = this.formulario.getRawValue();
    // TODO: Remover essa linha quando implementar o login
    entrega.funcionarioId = 1;

    let operacao: Observable<Entrega>;

    if (this.novaEntrega) {
      operacao = this.criarEntrega(entrega as Entrega);
    } else {
      entrega.id = this.entregaId;
      operacao = this.atualizarEntrega(entrega as Entrega);
    }

    operacao.subscribe({
      next: () => {
        this.toastr.success(`Entrega ${this.entregaId ? 'atualizada' : 'criada'} com sucesso!`, 'Sucesso!');
        this.router.navigate(['/entregas']);
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
      this.entregaId = Number(params.get('id')) || 0;

      if (this.entregaId == 0) {
        this.novaEntrega = true;
        this.titulo = 'Nova Entrega';
      } else {
        this.novaEntrega = false;
        this.titulo = 'Detalhes da Entrega';
      }
    });
  }

  private carregarEntrega(): void {
    if (!this.novaEntrega) {
      this.spinner.show();

      this.entregaService.obterEntregaPorId(this.entregaId).subscribe({
        next: (entrega: Entrega) => {
          this.entrega = { ...entrega };
          this.formulario.patchValue(this.entrega);
          this.titulo = 'Edição: '; // + this.entrega.nome;
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

  private carregarMoradores() {
    this.moradorService.obterMoradores().pipe(takeUntil(this.destroy$)).subscribe({
      next: (moradores: Morador[]) => {
        this.moradores = moradores;
      },
      error: (error: any) => {
        this.tratarErros(error);
      }
    });
  }

  private carregarTransportadoras() {
    this.transportadoraService.obterTransportadoras().pipe(takeUntil(this.destroy$)).subscribe({
      next: (transportadoras: Transportadora[]) => {
        this.transportadoras = transportadoras;
      },
      error: (error: any) => {
        this.tratarErros(error);
      }
    });
  }

  private validarformulario(): void {
    this.formulario = this.formBuilder.group({
      transportadoraId: ['', Validators.required],
      moradorId: ['', Validators.required],
      descricao: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      status: ['', Validators.required],
      dataRecebimento: [{ value: this.novaEntrega ? new Date() : '', disabled: this.novaEntrega }],
      dataRetirada: [''],
    });
  }

  private atualizarEntrega(entrega: Entrega): Observable<Entrega> {
    return this.entregaService.atualizar(entrega.id, entrega);
  }

  private criarEntrega(entrega: Entrega): Observable<Entrega> {
    return this.entregaService.criar(entrega);
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
}
