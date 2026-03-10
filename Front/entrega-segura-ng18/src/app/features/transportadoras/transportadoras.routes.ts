import { Routes } from '@angular/router';

export const transportadorasRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./transportadoras.component').then(c => c.TransportadorasComponent),
  },
];
