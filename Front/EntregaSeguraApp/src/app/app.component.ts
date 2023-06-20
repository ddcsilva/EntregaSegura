import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ContaService } from './services/usuario/conta.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public mensagemDeCarregamentoSelecionada: string = '';
  public isUserAuthenticated: boolean = false;

  constructor(public router: Router, public contaService: ContaService, private ref: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.selecionarMensagemDeCarregamento();
  
    this.contaService.currentUser$.subscribe(user => {
      this.isUserAuthenticated = !!user;
      this.ref.detectChanges();
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
    this.contaService.logout();
    this.isUserAuthenticated = false;
  }  
}
