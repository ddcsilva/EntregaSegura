import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginValidator } from '@app/helpers/validators/login-validator.validator';
import { AutenticacaoService } from '@app/services/autenticacao.service';
import { UsuarioService } from '@app/services/usuario.service';
import { TratamentoErrosService } from '@app/shared/services/tratamento-erros.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public formulario: FormGroup = new FormGroup({});
  public carregando = false;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    private readonly autenticacaoService: AutenticacaoService,
    private readonly toastr: ToastrService,
    private readonly tratamentoErrosService: TratamentoErrosService
  ) { }

  ngOnInit(): void {
    this.validarformulario();
  }

  public submeterFormulario(): void {
    if (this.formulario.valid) {
      this.carregando = true;

      this.autenticacaoService.autenticar(this.formulario.value).subscribe({
        next: (response) => {
          this.formulario.reset();
          this.carregando = false;
          this.autenticacaoService.armazenarToken(response.token);
          this.toastr.success('Login realizado com sucesso!', 'Sucesso!');
          this.router.navigate(['entregas']);
        },
        error: (error: any) => {
          this.formulario.reset();
          this.carregando = false;
          this.tratarErros(error);
        },
      });
    }
  }

  private validarformulario(): void {
    this.formulario = this.formBuilder.group({
      login: ['', [Validators.required, LoginValidator()]],
      senha: ['', [Validators.required]]
    });
  }

  private tratarErros(erro: any): void {
    this.tratamentoErrosService.tratarErro(erro).subscribe({
      error: (mensagemErro: any) => {
        if (mensagemErro.message && typeof mensagemErro.message === 'string') {
          const mensagensErro = mensagemErro.message.split(',');
          for (const mensagem of mensagensErro) {
            this.toastr.error(mensagem.trim(), 'Houve um erro!');
          }
        }
      }
    });
  }
}
