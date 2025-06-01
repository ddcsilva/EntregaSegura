import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'es-condominios',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="p-6">
      <h1 class="mb-6 text-2xl font-bold text-gray-900">Condomínios</h1>
      <div class="rounded-lg bg-white p-6 shadow">
        <p class="text-gray-600">Área de gerenciamento de condomínios</p>
      </div>
    </div>
  `,
  styles: [],
})
export class CondominiosComponent {}
