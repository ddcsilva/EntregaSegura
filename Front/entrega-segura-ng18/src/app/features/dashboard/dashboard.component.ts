import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'es-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="p-6">
      <h1 class="mb-6 text-2xl font-bold text-gray-900">Dashboard</h1>
      <div class="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-4">
        <div class="rounded-lg bg-white p-6 shadow">
          <h3 class="text-lg font-medium text-gray-900">Entregas Hoje</h3>
          <p class="mt-2 text-3xl font-bold text-blue-600">12</p>
        </div>
        <div class="rounded-lg bg-white p-6 shadow">
          <h3 class="text-lg font-medium text-gray-900">Pendentes</h3>
          <p class="mt-2 text-3xl font-bold text-yellow-600">5</p>
        </div>
        <div class="rounded-lg bg-white p-6 shadow">
          <h3 class="text-lg font-medium text-gray-900">Entregues</h3>
          <p class="mt-2 text-3xl font-bold text-green-600">7</p>
        </div>
        <div class="rounded-lg bg-white p-6 shadow">
          <h3 class="text-lg font-medium text-gray-900">Total Mês</h3>
          <p class="mt-2 text-3xl font-bold text-purple-600">156</p>
        </div>
      </div>
    </div>
  `,
  styles: [],
})
export class DashboardComponent {}
