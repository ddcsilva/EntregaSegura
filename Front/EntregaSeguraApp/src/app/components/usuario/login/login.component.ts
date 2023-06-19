import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Login } from 'src/app/models/login';
import { ContaService } from 'src/app/services/usuario/conta.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public model = {} as Login;


  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    senha: new FormControl('', [Validators.required])
  });

  constructor(private router: Router, private toastr: ToastrService, private contaService: ContaService) { }

  ngOnInit(): void {
  }

  public login(): void {
    this.contaService.login(this.model).subscribe({
      next: (usuario: any) => {
        this.router.navigate(['/']);
        this.toastr.success('Login realizado com sucesso!', 'Sucesso!');
      },
      error: (error: any) => {
        if (error.status === 401)
          this.toastr.error('Usuário e/ou senha incorretos', 'Erro!');
        else
          this.toastr.error('Erro ao tentar realizar o login', 'Erro!');
      }
    })
  }
}
