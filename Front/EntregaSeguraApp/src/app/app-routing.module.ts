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
import { MainComponent } from './components/main/main.component';
import { EntregasComponent } from './components/entregas/entregas.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DetalhesEntregaComponent } from './components/entregas/detalhes-entrega/detalhes-entrega.component';
import { LoginComponent } from './components/login/login.component';
import { AutenticacaoGuard } from './helpers/guards/autenticacao.guard';

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
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [AutenticacaoGuard]
      },
      {
        path: 'condominios',
        component: CondominiosComponent
      },
      {
        path: 'condominios/:id',
        component: DetalhesCondominioComponent
      },
      {
        path: 'unidades',
        component: UnidadesComponent
      },
      {
        path: 'unidades/:id',
        component: DetalhesUnidadeComponent
      },
      {
        path: 'moradores',
        component: MoradoresComponent
      },
      {
        path: 'moradores/:id',
        component: DetalhesMoradorComponent
      },
      {
        path: 'entregas',
        component: EntregasComponent
      },
      {
        path: 'entregas/:id',
        component: DetalhesEntregaComponent
      },
      {
        path: 'funcionarios',
        component: FuncionariosComponent
      },
      {
        path: 'funcionarios/:id',
        component: DetalhesFuncionarioComponent
      },
      {
        path: 'transportadoras',
        component: TransportadorasComponent
      },
      {
        path: 'transportadoras/:id',
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
