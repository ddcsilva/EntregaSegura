import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AutenticacaoService } from '@app/services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public mensagemDeCarregamentoSelecionada: string = '';
  public isUserAuthenticated: boolean = false;

  constructor(public router: Router, public autenticacaoService: AutenticacaoService, private ref: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.selecionarMensagemDeCarregamento();
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

  logout() {
    this.autenticacaoService.logout();
    this.router.navigate(['/login']);
  }  
}
