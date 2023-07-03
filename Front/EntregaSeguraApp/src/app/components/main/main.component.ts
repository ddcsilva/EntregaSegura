import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';
import { MatSidenav } from '@angular/material/sidenav';
import { delay, filter } from 'rxjs/operators';
import { NavigationEnd, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { NgxSpinnerService } from 'ngx-spinner';
import { AutenticacaoService } from '@app/services/autenticacao.service';
import { ToastrService } from 'ngx-toastr';
import { UsuarioService } from '@app/services/usuario.service';

@UntilDestroy()
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit, AfterViewInit {
  public mensagemDeCarregamentoSelecionada: string = '';
  public nomeUsuario: string = '';
  public perfilUsuario: string = '';
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;

  constructor(
    private observer: BreakpointObserver,
    private router: Router,
    private spinner: NgxSpinnerService,
    private readonly usuarioService: UsuarioService,
    private readonly autenticacaoService: AutenticacaoService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.selecionarMensagemDeCarregamento();
  
    this.usuarioService.obterNomeDaClaim().subscribe(nome => {
      this.nomeUsuario = nome;
    });
  
    this.usuarioService.obterPerfilDaClaim().subscribe(perfil => {
      this.perfilUsuario = perfil;
    });
  
    this.spinner.hide();
  }  

  ngAfterViewInit() {
    this.observer
      .observe(['(max-width: 800px)'])
      .pipe(delay(1), untilDestroyed(this))
      .subscribe((res) => {
        if (res.matches) {
          this.sidenav.mode = 'over';
          this.sidenav.close();
        } else {
          this.sidenav.mode = 'side';
          this.sidenav.open();
        }
      });

    this.router.events
      .pipe(
        untilDestroyed(this),
        filter((e) => e instanceof NavigationEnd)
      )
      .subscribe(() => {
        if (this.sidenav.mode === 'over') {
          this.sidenav.close();
        }
      });
  }

  selecionarMensagemDeCarregamento(): void {
    const index = Math.floor(Math.random() * this.mensagensDeCarregamento.length);
    this.mensagemDeCarregamentoSelecionada = this.mensagensDeCarregamento[index];
  }

  public mensagensDeCarregamento: string[] = [
    'Preparando tudo para você...',
    'Quase lá...',
    'Trabalhando duro...',
    'Quase pronto...',
    'Só um momento...',
    'Finalizando os últimos detalhes...'
  ];

  public get mensagemDeCarregamento(): string {
    return this.mensagemDeCarregamentoSelecionada;
  }

  public logout(): void {
    this.autenticacaoService.efetuarLogout();
  }
}
