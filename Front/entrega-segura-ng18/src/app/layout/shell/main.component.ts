import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

import { HeaderComponent } from '../header/header.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { NavegacaoService } from '../services/navegacao.service';

@Component({
  selector: 'es-main',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeaderComponent, SidebarComponent],
  templateUrl: './main.component.html',
})
export class MainComponent implements OnInit {
  public readonly navegacaoService = inject(NavegacaoService);

  ngOnInit(): void {
    if (!this.navegacaoService.mobile()) {
      this.navegacaoService.abrirSidebar();
    }
  }
}
