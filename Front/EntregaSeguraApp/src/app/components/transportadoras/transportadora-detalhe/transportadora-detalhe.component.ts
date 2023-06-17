// Angular imports
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

// Library imports
import { Observable } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

// Model imports
import { Transportadora } from 'src/app/models/transportadora';

// Service imports
import { TransportadoraService } from 'src/app/services/transportadora/transportadora.service';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

// Helper imports
import { ValidadorCampos } from 'src/app/helpers/ValidadorCampos';

@Component({
  selector: 'app-transportadora-detalhe',
  templateUrl: './transportadora-detalhe.component.html',
  styleUrls: ['./transportadora-detalhe.component.scss']
})
export class TransportadoraDetalheComponent implements OnInit {
  public titulo: string = 'Nova Transportadora';
  public mascaraTelefone: string = '(00) 0000-00009';
  public formulario: FormGroup = new FormGroup({});

  private id: number | null = null;
  private transportadora: Transportadora = {} as Transportadora;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private transportadoraService: TransportadoraService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private tratamentoErrosService: TratamentoErrosService) { }

  ngOnInit() {
    this.id = Number(this.route.snapshot.params['id']);

    this.validarformulario();
    this.carregarTransportadora();

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

    const transportadora: Partial<Transportadora> = this.formulario.getRawValue();

    let operacao: Observable<Transportadora>;

    if (this.id) {
      transportadora.id = this.id;
      operacao = this.atualizarTransportadora(transportadora as Transportadora);
    } else {
      operacao = this.criarTransportadora(transportadora as Transportadora);
    }

    operacao.subscribe({
      next: () => {
        this.toastr.success(`Transportadora ${this.id ? 'atualizado' : 'criado'} com sucesso!`, 'Sucesso');
        this.router.navigate(['/transportadoras']);
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

  private carregarTransportadora(): void {
    if (this.id) {
      this.spinner.show();

      this.transportadoraService.obterPorId(String(this.id)).subscribe({
        next: (transportadora: Transportadora) => {
          this.transportadora = { ...transportadora };
          this.formulario.patchValue(this.transportadora);
          this.titulo = 'Edição: ' + this.transportadora.nome;
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao carregar a transportadora', 'Erro!');
        },
        complete: () => this.spinner.hide()
      });
    }
  }

  private atualizarMascaraTelefone(value: string): void {
    if (value) {
      const numbers = value.replace(/\D/g, '');
      this.mascaraTelefone = numbers.length > 10 ? '(00) 00000-0000' : '(00) 0000-00009';
    } else {
      this.mascaraTelefone = '(00) 0000-00009';
    }
  }

  private atualizarTransportadora(transportadora: Transportadora): Observable<Transportadora> {
    return this.transportadoraService.atualizar(String(transportadora.id), transportadora);
  }

  private criarTransportadora(transportadora: Transportadora): Observable<Transportadora> {
    return this.transportadoraService.criar(transportadora);
  }

  private tratarErros(erro: any): void {
    this.spinner.hide();
    this.tratamentoErrosService.tratarErro(erro).subscribe({
      error: (error: any) => this.toastr.error(error.message, 'Erro!')
    });
  }

  private validarformulario(): void {
    this.formulario = this.formBuilder.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      cnpj: ['', [ValidadorCampos.ValidaCNPJ]],
      telefone: ['', [Validators.minLength(10)]],
      email: ['', [Validators.email, Validators.minLength(2)]]
    });
  }

  get formControl(): any {
    return this.formulario.controls;
  }
}
