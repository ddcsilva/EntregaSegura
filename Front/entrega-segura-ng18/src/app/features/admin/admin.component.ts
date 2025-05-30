import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'es-admin',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  template: `
    <div class="p-6">
      <h1 class="mb-6 text-2xl font-bold text-gray-900">Administração</h1>
      <div class="rounded-lg bg-white p-6 shadow">
        <p class="text-gray-600">Área administrativa do sistema</p>
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
  styles: [],
})
export class AdminComponent {}
