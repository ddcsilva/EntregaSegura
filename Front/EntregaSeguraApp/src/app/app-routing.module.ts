import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CondominiosComponent } from './components/condominios/condominios.component';
import { DetalhesCondominioComponent } from './components/condominios/detalhes-condominio/detalhes-condominio.component';
import { TransportadorasComponent } from './components/transportadoras/transportadoras.component';
import { DetalhesTransportadoraComponent } from './components/transportadoras/detalhes-transportadora/detalhes-transportadora.component';
import { UnidadesComponent } from './components/unidades/unidades.component';
import { DetalhesUnidadeComponent } from './components/unidades/detalhes-unidade/detalhes-unidade.component';
import { MoradoresComponent } from './components/moradores/moradores.component';
import { DetalhesMoradorComponent } from './components/moradores/detalhes-morador/detalhes-morador.component';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { DetalhesFuncionarioComponent } from './components/funcionarios/detalhes-funcionario/detalhes-funcionario.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './shared/helpers/auth.guard';
import { MainComponent } from './components/main/main.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '',
    component: MainComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        canActivate: [AuthGuard],
        component: CondominiosComponent
      },
      {
        path: 'condominios',
        canActivate: [AuthGuard],
        component: CondominiosComponent
      },
      {
        path: 'condominios/:id',
        canActivate: [AuthGuard],
        component: DetalhesCondominioComponent
      },
      {
        path: 'unidades',
        canActivate: [AuthGuard],
        component: UnidadesComponent
      },
      {
        path: 'unidades/:id',
        canActivate: [AuthGuard],
        component: DetalhesUnidadeComponent
      },
      {
        path: 'moradores',
        canActivate: [AuthGuard],
        component: MoradoresComponent
      },
      {
        path: 'moradores/:id',
        canActivate: [AuthGuard],
        component: DetalhesMoradorComponent
      },
      {
        path: 'funcionarios',
        canActivate: [AuthGuard],
        component: FuncionariosComponent
      },
      {
        path: 'funcionarios/:id',
        canActivate: [AuthGuard],
        component: DetalhesFuncionarioComponent
      },
      {
        path: 'transportadoras',
        canActivate: [AuthGuard],
        component: TransportadorasComponent
      },
      {
        path: 'transportadoras/:id',
        canActivate: [AuthGuard],
        component: DetalhesTransportadoraComponent
      }
    ]
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
