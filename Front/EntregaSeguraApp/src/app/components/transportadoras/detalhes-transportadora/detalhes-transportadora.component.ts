import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Transportadora } from '@app/models/transportadora.model';
import { TransportadoraService } from '@app/services/transportadora.service';
import { ValidadorCampos } from '@app/shared/helpers/validador-campos';
import { TratamentoErrosService } from '@app/shared/services/tratamento-erros.service';

@Component({
  selector: 'app-detalhes-transportadora',
  templateUrl: './detalhes-transportadora.component.html',
  styleUrls: ['./detalhes-transportadora.component.scss']
})
export class DetalhesTransportadoraComponent implements OnInit {
  public novaTransportadora: boolean = false;
  public titulo: string = '';
  public mascaraTelefone: string = '(00) 0000-00009';
  public formulario: FormGroup = new FormGroup({});

  private transportadoraId: number = 0;
  private transportadora: Transportadora = {} as Transportadora;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly transportadoraService: TransportadoraService,
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

    if (this.novaTransportadora) {
      operacao = this.criarTransportadora(transportadora as Transportadora);
    } else {
      transportadora.id = this.transportadoraId;
      operacao = this.atualizarTransportadora(transportadora as Transportadora);
    }

    operacao.subscribe({
      next: () => {
        this.toastr.success(`Transportadora ${this.transportadoraId ? 'atualizada' : 'criada'} com sucesso!`, 'Sucesso');
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

  private definirOperacao(): void {
    this.route.paramMap.subscribe(params => {
      this.transportadoraId = Number(params.get('id')) || 0;

      if (this.transportadoraId == 0) {
        this.novaTransportadora = true;
        this.titulo = 'Nova Transportadora';
      } else {
        this.novaTransportadora = false;
        this.titulo = 'Detalhes da Transportadora';
      }
    });
  }

  private carregarTransportadora(): void {
    if (!this.novaTransportadora) {
      this.spinner.show();

      this.transportadoraService.obterTransportadoraPorId(this.transportadoraId).subscribe({
        next: (transportadora: Transportadora) => {
          this.transportadora = { ...transportadora };
          this.formulario.patchValue(this.transportadora);
          this.titulo = 'Edição: ' + this.transportadora.nome;
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
      cnpj: ['', [ValidadorCampos.ValidaCNPJ]],
      email: ['', [Validators.email]],
      telefone: ['', [Validators.minLength(10)]]
    });
  }

  private atualizarTransportadora(transportadora: Transportadora): Observable<Transportadora> {
    return this.transportadoraService.atualizar(transportadora.id, transportadora);
  }

  private criarTransportadora(transportadora: Transportadora): Observable<Transportadora> {
    return this.transportadoraService.criar(transportadora);
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
