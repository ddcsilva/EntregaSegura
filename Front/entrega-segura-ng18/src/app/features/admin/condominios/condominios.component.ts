import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'es-condominios',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div>
      <h2 class="mb-4 text-xl font-semibold text-gray-900">Gerenciar Condomínios</h2>
      <p class="text-gray-600">Lista de condomínios cadastrados</p>
    </div>
  `,
  styles: [],
})
export class CondominiosComponent {}
