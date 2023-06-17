// Angular imports
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

// Library imports
import { Observable } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

// Model imports
import { Unidade } from 'src/app/models/unidade';
import { Condominio } from 'src/app/models/condominio';

// Service imports
import { UnidadeService } from 'src/app/services/unidade/unidade.service';
import { CondominioService } from 'src/app/services/condominio/condominio.service';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Component({
  selector: 'app-unidade-detalhe',
  templateUrl: './unidade-detalhe.component.html',
  styleUrls: ['./unidade-detalhe.component.scss']
})
export class UnidadeDetalheComponent implements OnInit {
  public titulo: string = 'Nova Unidade';
  public mascaraTelefone: string = '(00) 0000-00009';
  public formulario: FormGroup = new FormGroup({});
  public condominios: Condominio[] = [];

  private id: number | null = null;
  private unidade: Unidade = {} as Unidade;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private unidadeService: UnidadeService,
    private condominioService: CondominioService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private tratamentoErrosService: TratamentoErrosService) { }

  ngOnInit() {
    this.id = Number(this.route.snapshot.params['id']);

    this.validarformulario();
    this.carregarUnidade();
    this.obterCondominios();
  }

  public submeterFormulario(): void {
    this.spinner.show();
    if (this.formulario.invalid) {
      this.spinner.hide();
      return;
    }

    const unidade: Partial<Unidade> = this.formulario.getRawValue();

    let operacao: Observable<Unidade>;

    if (this.id) {
      unidade.id = this.id;
      operacao = this.atualizarUnidade(unidade as Unidade);
    } else {
      operacao = this.criarUnidade(unidade as Unidade);
    }

    operacao.subscribe({
      next: () => {
        this.toastr.success(`Unidade ${this.id ? 'atualizado' : 'criado'} com sucesso!`, 'Sucesso');
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

  private carregarUnidade(): void {
    if (this.id) {
      this.spinner.show();

      this.unidadeService.obterPorId(String(this.id)).subscribe({
        next: (unidade: Unidade) => {
          this.unidade = { ...unidade };
          this.formulario.patchValue(this.unidade);
          this.titulo = 'Edição: ' + this.unidade.descricaoUnidade;
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao carregar a unidade', 'Erro!');
        },
        complete: () => this.spinner.hide()
      });
    }
  }

  private atualizarUnidade(unidade: Unidade): Observable<Unidade> {
    return this.unidadeService.atualizar(String(unidade.id), unidade);
  }

  private criarUnidade(unidade: Unidade): Observable<Unidade> {
    return this.unidadeService.criar(unidade);
  }

  private tratarErros(erro: any): void {
    this.spinner.hide();
    this.tratamentoErrosService.tratarErro(erro).subscribe({
      error: (error: any) => this.toastr.error(error.message, 'Erro!')
    });
  }

  private validarformulario(): void {
    this.formulario = this.formBuilder.group({
      bloco: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(5)]],
      andar: ['', [Validators.required, Validators.min(1), Validators.max(40)]],
      numero: ['', [Validators.required, Validators.min(1), Validators.max(10)]],
      condominioId: ['', Validators.required]
    });
  }

  private obterCondominios() {
    this.condominioService.obterTodos().subscribe({
      next: (condominios: Condominio[]) => {
        this.condominios = condominios;
      },
      error: (error: any) => {
        this.exibirErros(error);
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

  get formControl(): any {
    return this.formulario.controls;
  }
}