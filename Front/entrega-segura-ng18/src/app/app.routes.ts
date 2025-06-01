import { Routes } from '@angular/router';
import { autenticacaoGuard, papelGuard } from '@core/guards';
import { Papel } from '@core/models';

export const routes: Routes = [
  {
    path: 'autenticacao',
    loadChildren: () => import('./features/autenticacao/autenticacao.routes').then(r => r.autenticacaoRoutes),
  },

  {
    path: '',
    loadComponent: () => import('./layout/shell/main.component').then(c => c.MainComponent),
    canActivate: [autenticacaoGuard],
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./features/dashboard/dashboard.component').then(c => c.DashboardComponent),
        title: 'Dashboard - EntregaSegura',
      },
      // {
      //   path: 'entregas',
      //   loadChildren: () => import('./features/entregas/entregas.routes').then(r => r.entregasRoutes),
      //   title: 'Entregas - EntregaSegura',
      // },
      // {
      //   path: 'condominios',
      //   loadChildren: () => import('./features/condominios/condominios.routes').then(r => r.condominiosRoutes),
      //   canActivate: [papelGuard([Papel.ADMIN, Papel.SINDICO])],
      //   title: 'Condomínios - EntregaSegura',
      // },
      // {
      //   path: 'moradores',
      //   loadChildren: () => import('./features/moradores/moradores.routes').then(r => r.moradoresRoutes),
      //   canActivate: [papelGuard([Papel.ADMIN, Papel.SINDICO])],
      //   title: 'Moradores - EntregaSegura',
      // },
      // {
      //   path: 'funcionarios',
      //   loadChildren: () => import('./features/funcionarios/funcionarios.routes').then(r => r.funcionariosRoutes),
      //   canActivate: [papelGuard([Papel.ADMIN])],
      //   title: 'Funcionários - EntregaSegura',
      // },
      // {
      //   path: 'transportadoras',
      //   loadChildren: () =>
      //     import('./features/transportadoras/transportadoras.routes').then(r => r.transportadorasRoutes),
      //   canActivate: [papelGuard([Papel.ADMIN, Papel.SINDICO, Papel.FUNCIONARIO])],
      //   title: 'Transportadoras - EntregaSegura',
      // },
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
    ],
  },

  // 🔄 Redirect padrão
  {
    path: '**',
    redirectTo: '/dashboard',
  },
];
