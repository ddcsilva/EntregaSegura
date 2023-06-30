import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Condominio } from '@app/models/condominio.model';
import { Funcionario } from '@app/models/funcionario.model';
import { CondominioService } from '@app/services/condominio.service';
import { FuncionarioService } from '@app/services/funcionario.service';
import { TratamentoErrosService } from '@app/shared/services/tratamento-erros.service';
import { ValidadorCampos } from '@app/shared/helpers/validador-campos';
import { Cargo } from '@app/models/enums/cargo.enum';

@Component({
  selector: 'app-detalhes-funcionario',
  templateUrl: './detalhes-funcionario.component.html',
  styleUrls: ['./detalhes-funcionario.component.scss']
})
export class DetalhesFuncionarioComponent implements OnInit, OnDestroy {
  public novoFuncionario: boolean = false;
  public titulo: string = '';
  public mascaraTelefone: string = '(00) 0000-00009';
  public formulario: FormGroup = new FormGroup({});
  public condominios: Condominio[] = [];
  public cargos = Object.values(Cargo);

  private funcionarioId: number = 0;
  private funcionario: Funcionario = {} as Funcionario;
  private destroy$ = new Subject<void>();

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly funcionarioService: FuncionarioService,
    private readonly condominioService: CondominioService,
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
    this.carregarFuncionario();
    this.validarformulario();

    this.formControl.telefone.valueChanges.subscribe((value: any) => {
      this.atualizarMascaraTelefone(value);
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

    const funcionario: Partial<Funcionario> = this.formulario.getRawValue();
    let operacao: Observable<Funcionario>;

    if (this.novoFuncionario) {
      const { dataDemissao, ...funcionarioSemDataDemissao } = funcionario;
      operacao = this.criarFuncionario(funcionarioSemDataDemissao as Funcionario);
    } else {
      funcionario.id = this.funcionarioId;
      operacao = this.atualizarFuncionario(funcionario as Funcionario);
    }

    operacao.subscribe({
      next: () => {
        this.toastr.success(`Funcionário ${this.funcionarioId ? 'atualizado' : 'criado'} com sucesso!`, 'Sucesso');
        this.router.navigate(['/funcionarios']);
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

  public limparData(control: FormControl) {
    control.setValue(null);
  }

  private definirOperacao(): void {
    this.route.paramMap.subscribe(params => {
      this.funcionarioId = Number(params.get('id')) || 0;

      if (this.funcionarioId == 0) {
        this.novoFuncionario = true;
        this.titulo = 'Novo Funcionário';
      } else {
        this.novoFuncionario = false;
        this.titulo = 'Detalhes do Funcionário';
      }
    });
  }

  private carregarFuncionario(): void {
    if (!this.novoFuncionario) {
      this.spinner.show();

      this.funcionarioService.obterFuncionarioPorId(this.funcionarioId).subscribe({
        next: (funcionario: Funcionario) => {
          this.funcionario = { ...funcionario };
          this.formulario.patchValue(this.funcionario);
          this.titulo = 'Edição: ' + this.funcionario.nome;
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
      condominioId: ['', Validators.required],
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      cpf: ['', [Validators.required, ValidadorCampos.ValidaCPF]],
      telefone: ['', [Validators.required, Validators.minLength(10)]],
      email: ['', [Validators.required, Validators.email]],
      cargo: ['', Validators.required],
      dataAdmissao: ['', Validators.required],
      dataDemissao: ['']
    });
  }

  private atualizarFuncionario(funcionario: Funcionario): Observable<Funcionario> {
    return this.funcionarioService.atualizar(funcionario.id, funcionario);
  }

  private criarFuncionario(funcionario: Funcionario): Observable<Funcionario> {
    return this.funcionarioService.criar(funcionario);
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
