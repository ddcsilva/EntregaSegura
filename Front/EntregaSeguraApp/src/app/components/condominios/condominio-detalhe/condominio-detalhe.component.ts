import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ValidadorCampos } from 'src/app/helpers/ValidadorCampos';
import { Condominio } from 'src/app/models/condominio';
import { CondominioService } from 'src/app/services/condominio/condominio.service';
import { CepService } from 'src/app/shared/services/cep/cep.service';

@Component({
  selector: 'app-condominio-detalhe',
  templateUrl: './condominio-detalhe.component.html',
  styleUrls: ['./condominio-detalhe.component.scss']
})
export class CondominioDetalheComponent implements OnInit {
  public titulo: string = 'Novo Condomínio';
  public mascaraTelefone = '(00) 00000-00009';
  public formulario: FormGroup = new FormGroup({});
  public id: number | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private condominioService: CondominioService,
    private cepService: CepService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.id = Number(this.route.snapshot.params['id']);
    if (this.id > 0) {
      this.titulo = 'Detalhe do Condomínio: ' + this.id;
    }

    this.validarformulario();

    this.formControl.telefone.valueChanges.subscribe((value: any) => {
      this.atualizarMascaraTelefone(value);
    });
  }

  get formControl(): any {
    return this.formulario.controls;
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
      next: (condominio: Condominio) => {
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
    if (Array.isArray(erro)) {
      erro.forEach((mensagemErro: string) => this.toastr.error(mensagemErro, 'Erro de Validação!'));
    } else {
      this.toastr.error(`Erro ao ${this.id ? 'atualizar' : 'criar'} o condomínio`, 'Erro!');
    }
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
