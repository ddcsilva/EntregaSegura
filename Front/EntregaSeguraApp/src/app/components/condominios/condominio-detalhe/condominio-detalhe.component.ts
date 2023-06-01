import { UnidadeService } from './../../../services/unidade/unidade.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ValidadorCampos } from 'src/app/helpers/ValidadorCampos';
import { Condominio } from 'src/app/models/condominio';
import { UnidadesEmMassa } from 'src/app/models/unidadesEmMassa';
import { CondominioService } from 'src/app/services/condominio/condominio.service';
import { ConfirmacaoDialogComponent } from 'src/app/shared/components/confirmacao-dialog/confirmacao-dialog.component';
import { InformacoesConfirmacaoDialog } from 'src/app/shared/models/informacoes-confirmacao-dialog';
import { CepService } from 'src/app/shared/services/cep/cep.service';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

@Component({
  selector: 'app-condominio-detalhe',
  templateUrl: './condominio-detalhe.component.html',
  styleUrls: ['./condominio-detalhe.component.scss']
})
export class CondominioDetalheComponent implements OnInit {
  public titulo: string = 'Novo Condomínio';
  public mascaraTelefone = '(00) 0000-00009';
  public formulario: FormGroup = new FormGroup({});
  public id: number | null = null;
  private condominio: Condominio = {} as Condominio;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private condominioService: CondominioService,
    private unidadeService: UnidadeService,
    private cepService: CepService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private tratamentoErrosService: TratamentoErrosService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.id = Number(this.route.snapshot.params['id']);

    this.validarformulario();
    this.carregarCondominio();

    this.formControl.telefone.valueChanges.subscribe((value: any) => {
      this.atualizarMascaraTelefone(value);
    });
  }

  get formControl(): any {
    return this.formulario.controls;
  }

  private carregarCondominio(): void {
    if (this.id) {
      this.spinner.show();

      this.condominioService.obterPorId(String(this.id)).subscribe({
        next: (condominio: Condominio) => {
          this.condominio = { ...condominio };
          this.formulario.patchValue(this.condominio);
          this.titulo = 'Edição: ' + this.condominio.nome;
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao carregar o condomínio', 'Erro!');
        },
        complete: () => this.spinner.hide()
      });
    }
  }

  public submeterFormulario(): void {
    this.spinner.show();
    if (this.formulario.invalid) {
      this.spinner.hide();
      return;
    }

    const condominio: Partial<Condominio> = this.formulario.getRawValue();

    let operacao: Observable<Condominio>;

    if (this.id) {
      condominio.id = this.id;
      operacao = this.atualizarCondominio(condominio as Condominio);
    } else {
      operacao = this.criarCondominio(condominio as Condominio);
    }

    operacao.subscribe({
      next: () => {

        if (!this.id) {
          const dialogConfig = {
            data: {
              titulo: 'Confirmação de Criação de Unidades',
              mensagem: `Para o condomínio "${condominio.nome}", você está prestes a criar ${condominio.quantidadeBlocos} blocos. Cada bloco terá ${condominio.quantidadeAndares} andares com ${condominio.quantidadeUnidades} unidades por andar. Deseja continuar?`,
              informacaoAdicional: 'Esta operação não pode ser desfeita.',
              textoBotaoCancelar: 'Cancelar',
              textoBotaoConfirmar: 'Confirmar',
            } as InformacoesConfirmacaoDialog
          };

          const dialogRef = this.dialog.open(ConfirmacaoDialogComponent, dialogConfig);

          dialogRef.afterClosed().subscribe(result => {
            if (result) {
              const unidadesEmMassaDTO: UnidadesEmMassa = {
                condominioId: this.id ? this.id : 0,
                quantidadeBlocos: condominio.quantidadeBlocos ? condominio.quantidadeBlocos : 0,
                quantidadeAndaresPorBloco: condominio.quantidadeAndares ? condominio.quantidadeAndares : 0,
                quantidadeUnidadesPorAndar: condominio.quantidadeUnidades ? condominio.quantidadeUnidades : 0
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

        this.toastr.success(`Condomínio ${this.id ? 'atualizado' : 'criado'} com sucesso!`, 'Sucesso');
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

  private atualizarMascaraTelefone(value: string): void {
    const numbers = value.replace(/\D/g, '');
    this.mascaraTelefone = numbers.length > 10 ? '(00) 00000-0000' : '(00) 0000-00009';
  }

  private atualizarCondominio(condominio: Condominio): Observable<Condominio> {
    return this.condominioService.atualizar(String(condominio.id), condominio);
  }

  private criarCondominio(condominio: Condominio): Observable<Condominio> {
    return this.condominioService.criar(condominio);
  }

  private tratarErros(erro: any): void {
    this.spinner.hide();
    this.tratamentoErrosService.tratarErro(erro).subscribe({
      error: (error: any) => this.toastr.error(error.message, 'Erro!')
    });
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

  private validarformulario(): void {
    this.formulario = this.formBuilder.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      cnpj: ['', [Validators.required, ValidadorCampos.ValidaCNPJ]],
      telefone: ['', [Validators.required, Validators.minLength(10)]],
      email: ['', [Validators.required, Validators.email, Validators.minLength(2)]],
      cep: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(8)]],
      logradouro: [{ value: '', disabled: true }],
      numero: ['', [Validators.required, Validators.pattern("^[0-9]*$"), Validators.maxLength(4)]],
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
}
