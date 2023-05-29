import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CepService } from 'src/app/shared/services/cep/cep.service';

@Component({
  selector: 'app-condominio-detalhe',
  templateUrl: './condominio-detalhe.component.html',
  styleUrls: ['./condominio-detalhe.component.scss']
})
export class CondominioDetalheComponent implements OnInit {
  public titulo: string = 'Novo Condomínio';

  condominioForm = this.fb.group({
    nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    cnpj: ['', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(14)]],
    telefone: ['', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.minLength(10), Validators.maxLength(11)]],
    email: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    cep: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(8)]],  // linha adicionada
    logradouro: [{ value: '', disabled: true }, Validators.required],
    numero: [{ value: '', disabled: true }, Validators.required],
    bairro: [{ value: '', disabled: true }, Validators.required],
    cidade: [{ value: '', disabled: true }, Validators.required],
    estado: [{ value: '', disabled: true }, Validators.required],
    quantidadeUnidades: ['', [Validators.required, Validators.min(1), Validators.max(1000)]],
    quantidadeAndares: ['', [Validators.required, Validators.min(1), Validators.max(50)]],
    quantidadeBlocos: ['', [Validators.required, Validators.min(1), Validators.max(50)]],
    });
    

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private cepService: CepService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.titulo = 'Detalhe do Condomínio: ' + id;
    }
  }

  submeterFormulario() {
    if (this.condominioForm.valid) {
      // Faça algo com os dados do formulário aqui
    }
  }

  get formControl(): any {
    return this.condominioForm.controls;
  }

  public buscarCep(event: Event) {
    event.preventDefault();
    const cep = this.condominioForm.get('cep')?.value;
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

            this.condominioForm.patchValue({
              logradouro: dados.logradouro,
              bairro: dados.bairro,
              cidade: dados.localidade,
              estado: dados.uf
            });

            this.desabilitarCamposEndereco();
          }
        },
        error: erro => {
          this.spinner.hide();
          this.toastr.error('Erro ao buscar o CEP!', 'Erro');

          this.habilitarCamposEndereco();
        }
      });
    }
  }

  private habilitarCamposEndereco() {
    this.condominioForm.get('logradouro')?.enable();
    this.condominioForm.get('bairro')?.enable();
    this.condominioForm.get('cidade')?.enable();
    this.condominioForm.get('estado')?.enable();
  }

  private desabilitarCamposEndereco() {
    this.condominioForm.get('logradouro')?.disable();
    this.condominioForm.get('bairro')?.disable();
    this.condominioForm.get('cidade')?.disable();
    this.condominioForm.get('estado')?.disable();
  }


}
