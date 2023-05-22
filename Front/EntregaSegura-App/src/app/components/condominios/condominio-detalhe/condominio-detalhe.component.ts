import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ValidadorCampos } from '@app/helpers/ValidadorCampos';
import { Condominio } from '@app/models/Condominio';
import { CondominioService } from '@app/services/condominio.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-condominio-detalhe',
  templateUrl: './condominio-detalhe.component.html',
  styleUrls: ['./condominio-detalhe.component.scss']
})
export class CondominioDetalheComponent implements OnInit {
  public condominio: Condominio = {} as Condominio;
  public formulario: FormGroup = new FormGroup({});
  public estados: string[] = ['AC', 'AL', 'AP', 'AM', 'BA', 'CE', 'DF', 'ES', 'GO', 'MA', 'MT', 'MS', 'MG', 'PA', 'PB', 'PR', 'PE', 'PI', 'RJ', 'RN', 'RS', 'RO', 'RR', 'SC', 'SP', 'SE', 'TO'];
  public id: number | null = null;
  public mascaraTelefone: string = '(00) 0000-00009';

  get formControl(): any {
    return this.formulario.controls;
  }

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private condominioService: CondominioService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.carregarCondominio();
    this.validarFormulario();

    this.formControl.telefone.valueChanges.subscribe((value: any) => {
      this.atualizarMascara(value);
    });
  }

  public atualizarMascara(value: string): void {
    const numbers = value.replace(/\D/g, '');
    this.mascaraTelefone = numbers.length > 10 ? '(00) 00000-0000' : '(00) 0000-00009';
  }

  public carregarCondominio(): void {
    if (this.id) {
      this.spinner.show();

      this.condominioService.obterPorId(String(this.id)).subscribe({
        next: (condominio: Condominio) => {
          this.condominio = { ...condominio };
          this.formulario.patchValue(this.condominio);
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao carregar o condomínio', 'Erro!');
        },
        complete: () => this.spinner.hide()
      });
    }
  }

  public validarFormulario(): void {
    this.formulario = this.formBuilder.group({
      quantidadeUnidades: ['', [Validators.required, Validators.min(1), Validators.max(10)]],
      quantidadeAndares: ['', [Validators.required, Validators.min(1), Validators.max(40)]],
      quantidadeBlocos: ['', [Validators.required, Validators.min(1), Validators.max(20)]],
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      cnpj: ['', [Validators.required, ValidadorCampos.ValidaCNPJ]],
      telefone: ['', [Validators.required, Validators.minLength(10)]],
      email: ['', [Validators.required, Validators.email, Validators.minLength(2)]],
      logradouro: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      numero: ['', [Validators.required, Validators.pattern("^[0-9]*$"), Validators.min(1), Validators.max(9999)]],
      complemento: ['', [Validators.minLength(3), Validators.maxLength(50)]],
      cep: ['', [Validators.required, Validators.minLength(8)]],
      bairro: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      cidade: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      estado: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(2)]]
    });
  }

  public reiniciarFormulario(event: any): void {
    event.preventDefault();
    this.formulario.reset();
  }

  public submeterFormulario(): void {
    this.spinner.show();
    if (this.formulario.invalid) {
      this.spinner.hide();
      return;
    }

    this.condominio = (this.formulario.value as Condominio);

    let operacao: Observable<Condominio>;

    if (this.id) {
      this.condominio.id = this.id;
      operacao = this.condominioService.atualizar(String(this.id), this.condominio);
    } else {
      operacao = this.condominioService.criar(this.condominio);
    }

    operacao.subscribe({
      next: (condominio: Condominio) => {
        this.toastr.success(`Condomínio ${this.id ? 'atualizado' : 'criado'} com sucesso!`, 'Sucesso');
        this.router.navigate(['/condominios']);
      },
      error: (error: any) => {
        this.spinner.hide();
        if (Array.isArray(error)) {
          error.forEach((errMsg: string) => this.toastr.error(errMsg, 'Erro de Validação!'));
        } else {
          this.toastr.error(`Erro ao ${this.id ? 'atualizar' : 'criar'} o condomínio`, 'Erro!');
        }
      },
      complete: () => this.spinner.hide()
    });
  }

  public gerarClassesValidacao(campoFormulario: FormControl): any {
    return {
      'is-invalid': campoFormulario.errors && campoFormulario.touched,
      'is-valid': !campoFormulario.errors && campoFormulario.touched
    };
  }

  voltarALista(event: Event) {
    event.preventDefault();
    this.router.navigate(['/condominios']);
  }
}
