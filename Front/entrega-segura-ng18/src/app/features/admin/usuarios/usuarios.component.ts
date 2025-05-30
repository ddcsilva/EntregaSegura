import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'es-usuarios',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div>
      <h2 class="mb-4 text-xl font-semibold text-gray-900">Gerenciar Usuários</h2>
      <p class="text-gray-600">Lista de usuários do sistema</p>
    </div>
  `,
  styles: [],
})
export class UsuariosComponent {}
