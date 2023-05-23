import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  isCollapsed = false;
  notificationCount = 7;
  userImagePath = '/assets/foto.jpg';

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  public exibirMenu(): boolean {
    return this.router.url !== '/usuarios/login';
  }  

  getUserInitials() {
    let name = "Danilo Silva"; //Substitua isso com a variável real do nome do usuário
    let names = name.split(' '),
      initials = names[0].substring(0, 1).toUpperCase();

    if (names.length > 1) {
      initials += names[names.length - 1].substring(0, 1).toUpperCase();
    }
    return initials;
  }
}
