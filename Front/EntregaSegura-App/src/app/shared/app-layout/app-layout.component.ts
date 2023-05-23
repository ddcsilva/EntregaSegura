import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './app-layout.component.html',
  styleUrls: ['./app-layout.component.scss']
})
export class AppLayoutComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  public exibirMenu(): boolean {
    return this.router.url !== '/usuarios/login';
  }
}
