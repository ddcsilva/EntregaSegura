import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Condominio } from '@app/models/condominio.model';
import { UnidadesEmMassa } from '@app/models/unidades-em-massa.model';
import { CondominioService } from '@app/services/condominio.service';
import { UnidadeService } from '@app/services/unidade.service';
import { ConfirmacaoDialogComponent } from '@app/shared/components/confirmacao-dialog/confirmacao-dialog.component';
import { ValidadorCampos } from '@app/shared/helpers/validador-campos';
import { InformacoesConfirmacaoDialog } from '@app/shared/models/InformacoesConfirmacaoDialog.model';
import { CepService } from '@app/shared/services/cep.service';
import { TratamentoErrosService } from '@app/shared/services/tratamento-erros.service';

@Component({
  selector: 'app-detalhes-condominio',
  templateUrl: './detalhes-condominio.component.html',
  styleUrls: ['./detalhes-condominio.component.scss']
})
export class DetalhesCondominioComponent implements OnInit {
  public titulo: string = '';
  public novoCondominio: boolean = false;
  public mascaraTelefone: string = '(00) 0000-00009';
  public formulario: FormGroup = new FormGroup({});

  private condominioId: number = 0;
  private condominio: Condominio = {} as Condominio;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly condominioService: CondominioService,
    private readonly unidadeService: UnidadeService,
    private readonly cepService: CepService,
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
    this.carregarCondominio();

    this.formControl.telefone.valueChanges.subscribe((value: any) => {
      this.atualizarMascaraTelefone(value);
    });
  }

  public submeterFormulario(): void {
    this.spinner.show();

    if (this.formulario.invalid) {
      this.spinner.hide();
      return;
    }

    const condominio: Partial<Condominio> = this.formulario.getRawValue();
    let operacao: Observable<Condominio>;

    if (this.novoCondominio) {
      operacao = this.criarCondominio(condominio as Condominio);
    } else {
      condominio.id = this.condominioId;
      operacao = this.atualizarCondominio(condominio as Condominio);
    }

    operacao.subscribe({
      next: (response: any) => {
        this.condominio = response;

        if (this.novoCondominio) {
          const dialogConfig = {
            data: {
              titulo: 'Confirmação de Criação de Unidades',
              mensagem: `Para o condomínio "${this.condominio.nome}", você está prestes a criar ${this.condominio.quantidadeBlocos} blocos. Cada bloco terá ${this.condominio.quantidadeAndares} andares com ${this.condominio.quantidadeUnidades} unidades por andar. Deseja continuar?`,
              informacaoAdicional: 'Esta operação não pode ser desfeita.',
              textoBotaoCancelar: 'Cancelar',
              textoBotaoConfirmar: 'Confirmar',
            } as InformacoesConfirmacaoDialog
          };

          const dialogRef = this.dialog.open(ConfirmacaoDialogComponent, dialogConfig);

          dialogRef.afterClosed().subscribe(result => {
            if (result) {
              const unidadesEmMassaDTO: UnidadesEmMassa = {
                condominioId: this.condominio.id ? this.condominio.id : 0,
                quantidadeBlocos: this.condominio.quantidadeBlocos ? this.condominio.quantidadeBlocos : 0,
                quantidadeAndaresPorBloco: this.condominio.quantidadeAndares ? this.condominio.quantidadeAndares : 0,
                quantidadeUnidadesPorAndar: this.condominio.quantidadeUnidades ? this.condominio.quantidadeUnidades : 0
              };

              this.unidadeService.adicionarEmMassa(unidadesEmMassaDTO).subscribe({
                next: () => {
                  this.toastr.success('Unidades criadas com sucesso!', 'Sucesso');
                },
                error: (error: any) => this.tratarErros(error)
              });
            }
          });
        }

        this.toastr.success(`Condomínio ${this.condominioId ? 'atualizado' : 'criado'} com sucesso!`, 'Sucesso');
        this.router.navigate(['/condominios']);
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

  public buscarCep(event: Event) {
    event.preventDefault();
    const cep = this.formulario.get('cep')?.value;
    if (cep) {
      this.spinner.show();
      this.cepService.buscarPorCep(cep).subscribe({
        next: dados => {
          this.spinner.hide();
          if (dados.erro) {
            this.toastr.error('CEP não encontrado!', 'Erro');
            this.habilitarCamposEndereco();
          } else {
            this.habilitarCamposEndereco();

            this.formulario.patchValue({
              logradouro: dados.logradouro,
              bairro: dados.bairro,
              cidade: dados.localidade,
              estado: dados.uf
            });

            this.desabilitarCamposEndereco();
          }
        },
        error: () => {
          this.spinner.hide();
          this.toastr.error('Erro ao buscar o CEP!', 'Erro');

          this.habilitarCamposEndereco();
        }
      });
    }
  }

  private definirOperacao(): void {
    this.route.paramMap.subscribe(params => {
      this.condominioId = Number(params.get('id')) || 0;

      if (this.condominioId == 0) {
        this.novoCondominio = true;
        this.titulo = 'Novo Condomínio';
      } else {
        this.novoCondominio = false;
        this.titulo = 'Detalhes do Condomínio';
      }
    });
  }

  private carregarCondominio(): void {
    if (!this.novoCondominio) {
      this.spinner.show();

      this.condominioService.ObterCondominioPorId(this.condominioId).subscribe({
        next: (condominio: Condominio) => {
          this.condominio = { ...condominio };
          this.formulario.patchValue(this.condominio);
          this.titulo = 'Edição: ' + this.condominio.nome;
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

  private validarformulario(): void {
    this.formulario = this.formBuilder.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      cnpj: ['', [Validators.required, ValidadorCampos.ValidaCNPJ]],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', [Validators.required, Validators.minLength(10)]],
      cep: ['', [Validators.required, Validators.minLength(8)]],
      logradouro: [{ value: '', disabled: true }],
      numero: ['', [Validators.required, Validators.pattern("^[1-9][0-9]*$")]],
      bairro: [{ value: '', disabled: true }],
      cidade: [{ value: '', disabled: true }],
      estado: [{ value: '', disabled: true }],
      quantidadeUnidades: ['', [Validators.required, Validators.min(1), Validators.max(10)]],
      quantidadeAndares: ['', [Validators.required, Validators.min(1), Validators.max(40)]],
      quantidadeBlocos: ['', [Validators.required, Validators.min(1), Validators.max(20)]],
    });
  }

  private habilitarCamposEndereco() {
    this.formulario.get('logradouro')?.enable();
    this.formulario.get('logradouro')?.setValidators([Validators.required]);

    this.formulario.get('bairro')?.enable();
    this.formulario.get('bairro')?.setValidators([Validators.required, Validators.minLength(3), Validators.maxLength(50)]);

    this.formulario.get('cidade')?.enable();
    this.formulario.get('cidade')?.setValidators([Validators.required, Validators.minLength(3), Validators.maxLength(50)]);

    this.formulario.get('estado')?.enable();
    this.formulario.get('estado')?.setValidators([Validators.minLength(2), Validators.maxLength(2)]);
  }

  private desabilitarCamposEndereco() {
    this.formulario.get('logradouro')?.disable();
    this.formulario.get('bairro')?.disable();
    this.formulario.get('cidade')?.disable();
    this.formulario.get('estado')?.disable();
  }

  private atualizarCondominio(condominio: Condominio): Observable<Condominio> {
    return this.condominioService.atualizar(condominio.id, condominio);
  }

  private criarCondominio(condominio: Condominio): Observable<Condominio> {
    return this.condominioService.criar(condominio);
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
