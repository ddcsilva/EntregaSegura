import { Routes } from '@angular/router';

export const moradoresRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./moradores.component').then(c => c.MoradoresComponent),
  },
];
