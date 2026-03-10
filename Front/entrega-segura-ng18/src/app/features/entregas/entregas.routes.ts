import { Routes } from '@angular/router';

export const entregasRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./entregas.component').then(c => c.EntregasComponent),
  },
];
