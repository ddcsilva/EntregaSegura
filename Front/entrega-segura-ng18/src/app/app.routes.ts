import { Routes } from '@angular/router';
import { autenticacaoGuard, papelGuard } from '@core/guards';
import { Papel } from '@core/models';

export const routes: Routes = [
  {
    path: 'autenticacao',
    loadChildren: () => import('./features/autenticacao/autenticacao.routes').then(r => r.autenticacaoRoutes),
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./features/dashboard/dashboard.component').then(c => c.DashboardComponent),
    canActivate: [autenticacaoGuard],
    title: 'Dashboard - EntregaSegura',
  },
  {
    path: 'admin',
    loadChildren: () => import('./features/admin/admin.routes').then(r => r.adminRoutes),
    canActivate: [papelGuard([Papel.ADMIN])],
    title: 'Administração - EntregaSegura',
  },
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: '/dashboard',
  },
];
