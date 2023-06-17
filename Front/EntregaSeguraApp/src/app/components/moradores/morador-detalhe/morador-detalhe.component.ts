// Angular imports
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

// Library imports
import { Observable } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

// Model imports
import { Morador } from 'src/app/models/morador';

// Service imports
import { MoradorService } from 'src/app/services/morador/morador.service';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';

// Helper imports
import { ValidadorCampos } from 'src/app/helpers/ValidadorCampos';

@Component({
  selector: 'app-morador-detalhe',
  templateUrl: './morador-detalhe.component.html',
  styleUrls: ['./morador-detalhe.component.scss']
})
export class MoradorDetalheComponent implements OnInit {
  public titulo: string = 'Novo Morador';
  public mascaraTelefone: string = '(00) 0000-00009';
  public nomeArquivo: string = '';
  public previewURL: any = null;
  public formulario: FormGroup = new FormGroup({});

  private id: number | null = null;
  private morador: Morador = {} as Morador;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private moradorService: MoradorService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private tratamentoErrosService: TratamentoErrosService) { }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.params['id']);

    this.validarformulario();
    this.carregarMorador();

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

    const morador: Partial<Morador> = this.formulario.getRawValue();

    let operacao: Observable<Morador>;

    if (this.id) {
      morador.id = this.id;
      operacao = this.atualizarMorador(morador as Morador);
    } else {
      operacao = this.criarMorador(morador as Morador);
    }

    operacao.subscribe({
      next: () => {
        this.toastr.success(`Morador ${this.id ? 'atualizado' : 'criado'} com sucesso!`, 'Sucesso');
        this.router.navigate(['/moradores']);
      },
      error: (error: any) => this.tratarErros(error),
      complete: () => this.spinner.hide()
    });
  }

  onFileSelect(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;

    if (files && files[0]) {
      if (!files[0].type.startsWith('image/')) {
        this.toastr.error('Por favor, selecione um arquivo de imagem.', 'Erro!');
        return;
      }

      const reader = new FileReader();

      reader.onload = (e: any) => this.previewURL = e.target.result;

      reader.readAsDataURL(files[0]);

      this.nomeArquivo = files[0].name;
    }
  }

  public reiniciarFormulario(event: any): void {
    event.preventDefault();
    this.formulario.reset();
    this.formulario.markAsPristine();
    this.formulario.markAsUntouched();
    this.nomeArquivo = '';
    this.previewURL = null;
  }

  private atualizarMascaraTelefone(value: string): void {
    if (value) {
      const numbers = value.replace(/\D/g, '');
      this.mascaraTelefone = numbers.length > 10 ? '(00) 00000-0000' : '(00) 0000-00009';
    } else {
      this.mascaraTelefone = '(00) 0000-00009';
    }
  }

  private carregarMorador(): void {
    if (this.id) {
      this.spinner.show();

      this.moradorService.obterPorId(String(this.id)).subscribe({
        next: (morador: Morador) => {
          this.morador = { ...morador };
          this.formulario.patchValue(this.morador);
          this.titulo = 'Edição: ' + this.morador.nome;
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao carregar o morador', 'Erro!');
        },
        complete: () => this.spinner.hide()
      });
    }
  }

  private atualizarMorador(morador: Morador): Observable<Morador> {
    return this.moradorService.atualizar(String(morador.id), morador);
  }

  private criarMorador(morador: Morador): Observable<Morador> {
    return this.moradorService.criar(morador);
  }

  private tratarErros(erro: any): void {
    this.spinner.hide();
    this.tratamentoErrosService.tratarErro(erro).subscribe({
      error: (error: any) => this.toastr.error(error.message, 'Erro!')
    });
  }

  private validarformulario(): void {
    this.formulario = this.formBuilder.group({
      nome: ['', Validators.required],
      cpf: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      ramal: [''],
      foto: ['']
    });
  }

  get formControl(): any {
    return this.formulario.controls;
  }
}
