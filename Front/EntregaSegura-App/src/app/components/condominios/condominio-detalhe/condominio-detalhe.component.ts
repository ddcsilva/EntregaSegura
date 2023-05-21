import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Condominio } from '@app/models/Condominio';
import { CondominioService } from '@app/services/condominio.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-condominio-detalhe',
  templateUrl: './condominio-detalhe.component.html',
  styleUrls: ['./condominio-detalhe.component.scss']
})
export class CondominioDetalheComponent implements OnInit {
  condominio: Condominio = {} as Condominio;
  formulario: FormGroup = new FormGroup({});

  get formControl(): any {
    return this.formulario.controls;
  }

  constructor(private formBuilder: FormBuilder,
    private router: ActivatedRoute,
    private condominioService: CondominioService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.carregarCondominio();
    this.validarFormulario();
  }

  public carregarCondominio(): void {
    const id = this.router.snapshot.paramMap.get('id');

    if (id) {
      this.spinner.show();

      this.condominioService.obterPorId(id).subscribe({
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
      quantidadeUnidades: ['', Validators.required],
      quantidadeAndares: ['', Validators.required],
      quantidadeBlocos: ['', Validators.required],
      nome: ['', Validators.required],
      cnpj: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      logradouro: ['', Validators.required],
      numero: ['', Validators.required],
      complemento: ['', Validators.required],
      cep: ['', Validators.required],
      bairro: ['', Validators.required],
      cidade: ['', Validators.required],
      estado: ['', Validators.required]
    });
  }

  public reiniciarFormulario(event: any): void {
    event.preventDefault();
    this.formulario.reset();
  }

  public submeterFormulario(): void {
    if (this.formulario.invalid) {
      return;
    }
  }

  public gerarClassesValidacao(campoFormulario: FormControl): any {
    return {
      'is-invalid': campoFormulario.errors && campoFormulario.touched,
      'is-valid': !campoFormulario.errors && campoFormulario.touched
    };
  }
}
