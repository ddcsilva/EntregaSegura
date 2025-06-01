import { Routes } from '@angular/router';

export const funcionariosRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./funcionarios.component').then(c => c.FuncionariosComponent),
  },
];
