import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ContaService } from 'src/app/services/usuario/conta.service';
import { SidenavService } from '../../services/sidenav-service.service';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit, AfterViewInit {
  @ViewChild('sidenav', { static: false }) sidenav!: MatSidenav;
  public mediaQueryDispositivoMovel: MediaQueryList;
  public sidenavAberto: boolean;
  fotoPerfilUsuario: string = '/assets/images/silvio-santos.jpg';
  iniciaisUsuario: string = '';

  constructor(private sidenavService: SidenavService, public contaService: ContaService, public router: Router, private changeDetectorRef: ChangeDetectorRef) {
    this.mediaQueryDispositivoMovel = window.matchMedia('(max-width: 600px)');
    this.sidenavAberto = !this.mediaQueryDispositivoMovel.matches;
  }

  ngOnInit(): void {
    this.definirIniciaisUsuario();
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

  ngAfterViewInit(): void {
    this.mediaQueryDispositivoMovel.addEventListener('change', () => {
      this.sidenavAberto = !this.mediaQueryDispositivoMovel.matches;
      this.changeDetectorRef.detectChanges();
    });
  }

  private definirIniciaisUsuario(): void {
    let nomes = 'Danilo Silva'.split(' ');
    this.iniciaisUsuario = nomes[0].charAt(0) + nomes[nomes.length - 1].charAt(0);
  }

  public logout(): void {
    this.contaService.logout();
    this.router.navigateByUrl('/usuario/login');
  }
}
