import { Usuario } from '@app/models/usuario';
import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';

import { AutenticacaoService } from '@app/services';
import { SidenavService } from '@app/shared/services';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('sidenav', { static: false }) sidenav!: MatSidenav;
  public mediaQueryDispositivoMovel: MediaQueryList;
  public sidenavAberto: boolean;
  public fotoPerfilUsuario: string = '';
  public iniciaisUsuario: string = '';
  public usuario: Usuario;

  private subscription: Subscription; 

  constructor(private sidenavService: SidenavService, public autenticacaoService: AutenticacaoService, public router: Router, private changeDetectorRef: ChangeDetectorRef) {
    this.mediaQueryDispositivoMovel = window.matchMedia('(max-width: 600px)');
    this.sidenavAberto = !this.mediaQueryDispositivoMovel.matches;
    this.usuario = <Usuario>this.autenticacaoService.usuarioAutenticado;

    this.subscription = this.autenticacaoService.usuario.subscribe(usuario => {
      this.usuario = usuario as Usuario;
      if (usuario) {
        this.fotoPerfilUsuario = this.usuario.foto ? `/assets/images/${this.usuario.foto}` : '';
        this.definirIniciaisUsuario();
      } else {
        this.iniciaisUsuario = '';
      }
    });
  }

  ngOnInit(): void {    
    this.sidenavService.sidenavOpen$.subscribe(open => {
      if (this.sidenav) {
        if (open) {
          this.sidenav.open();
        } else {
          this.sidenav.close();
        }
      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  ngAfterViewInit(): void {
    this.mediaQueryDispositivoMovel.addEventListener('change', () => {
      this.sidenavAberto = !this.mediaQueryDispositivoMovel.matches;
      this.changeDetectorRef.detectChanges();
    });
  }

  private definirIniciaisUsuario(): void {
    let nomes = this.usuario.nome.split(' ');

    if (nomes.length > 1) {
      this.iniciaisUsuario = nomes[0].charAt(0) + nomes[nomes.length - 1].charAt(0);
    } else {
      this.iniciaisUsuario = nomes[0].charAt(0);
    }
  }
  

  public logout(): void {
    this.autenticacaoService.logout();
    this.router.navigateByUrl('/login');
  }
}
