import { Routes } from '@angular/router';

export const condominiosRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./condominios.component').then(c => c.CondominiosComponent),
  },
];
