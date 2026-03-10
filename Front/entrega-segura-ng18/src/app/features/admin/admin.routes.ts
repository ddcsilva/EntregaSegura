import { Routes } from '@angular/router';

export const adminRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./admin.component').then(c => c.AdminComponent),
  },
  {
    path: 'usuarios',
    loadComponent: () => import('./usuarios/usuarios.component').then(c => c.UsuariosComponent),
  },
  {
    path: 'condominios',
    loadComponent: () => import('./condominios/condominios.component').then(c => c.CondominiosComponent),
  },
];
