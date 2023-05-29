import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterViewInit, OnDestroy {
  public mensagemDeCarregamentoSelecionada: string = '';
  public mediaQueryDispositivoMovel: MediaQueryList;
  public fotoPerfilUsuario: string = '';
  public iniciaisUsuario: string = '';

  @ViewChild('sidenav', { static: false }) sidenav!: MatSidenav;

  constructor(private router: Router, public route: ActivatedRoute, private changeDetectorRef: ChangeDetectorRef) {
    this.mediaQueryDispositivoMovel = window.matchMedia('(max-width: 600px)');
  }

  get dispositivoMovel(): boolean {
    return this.mediaQueryDispositivoMovel.matches;
  }

  set dispositivoMovel(value: boolean) {
    this.sidenav.opened = !value;
    this.changeDetectorRef.detectChanges();
  }

  private definirIniciaisUsuario(): void {
    let nomes = 'Danilo Silva'.split(' ');
    this.iniciaisUsuario = nomes[0].charAt(0) + nomes[nomes.length - 1].charAt(0);
  }

  ngOnInit(): void {
    this.definirIniciaisUsuario();
    this.selecionarMensagemDeCarregamento();
  }

  ngAfterViewInit(): void {
    this.mediaQueryDispositivoMovel.addEventListener('change', () => {
      this.dispositivoMovel = this.mediaQueryDispositivoMovel.matches;
    });

    this.dispositivoMovel = this.mediaQueryDispositivoMovel.matches;
  }

  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
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

}
