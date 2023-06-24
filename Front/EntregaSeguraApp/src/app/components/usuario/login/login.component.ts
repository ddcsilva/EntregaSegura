import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { AutenticacaoService } from "@app/services";
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { TratamentoErrosService } from 'src/app/shared/services/tratamento-erros/tratamento-erros.service';
import { first } from "rxjs";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    loginForm!: FormGroup;
    carregando = false;
    formularioEnviado = false;
    erro = '';

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private autenticacaoService: AutenticacaoService,
        private toastr: ToastrService,
        private spinner: NgxSpinnerService,
        private tratamentoErrosService: TratamentoErrosService
    ) {
        // Redireciona para a página inicial se já estiver conectado
        if (this.autenticacaoService.usuarioAutenticado) {
            this.router.navigate(['/']);
        }
    }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            senha: ['', [Validators.required]]
        });
    }

    get f() { return this.loginForm.controls; }

    efetuarLogin() {
        this.spinner.show();

        if (this.loginForm.invalid) {
            this.spinner.hide();
            return;
        }

        this.autenticacaoService.login(this.f['email'].value, this.f['senha'].value)
            .pipe(first())
            .subscribe({
                next: () => {
                    // Obtenha o URL de retorno dos parâmetros de consulta ou padrão para a página inicial
                    const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
                    this.router.navigateByUrl(returnUrl);
                    this.toastr.success('Login efetuado com sucesso!', 'Sucesso');
                    this.spinner.hide();
                },
                error: error => {
                    this.tratarErros(error);
                }
            });
    }

    private tratarErros(erro: any): void {
        this.spinner.hide();
        this.tratamentoErrosService.tratarErro(erro).subscribe({
            error: (error: any) => this.toastr.error(error.message, 'Erro!')
        });
    }
}
