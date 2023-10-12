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
import { PossuiPerfilGuard } from './helpers/guards/possui-perfil.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/entregas',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '',
    component: MainComponent,
    canActivate: [AutenticacaoGuard],
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent
      },
      {
        path: 'condominios',
        component: CondominiosComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico']
        }
      },
      {
        path: 'condominios/:id',
        component: DetalhesCondominioComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico']
        }
      },
      {
        path: 'unidades',
        component: UnidadesComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico']
        }
      },
      {
        path: 'unidades/:id',
        component: DetalhesUnidadeComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico']
        }
      },
      {
        path: 'moradores',
        component: MoradoresComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico']
        }
      },
      {
        path: 'moradores/:id',
        component: DetalhesMoradorComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico']
        }
      },
      {
        path: 'entregas',
        component: EntregasComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico', 'Funcionario', 'Morador']
        }
      },
      {
        path: 'entregas/:id',
        component: DetalhesEntregaComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico', 'Funcionario']
        }
      },
      {
        path: 'funcionarios',
        component: FuncionariosComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador']
        }
      },
      {
        path: 'funcionarios/:id',
        component: DetalhesFuncionarioComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador']
        }
      },
      {
        path: 'transportadoras',
        component: TransportadorasComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico', 'Funcionario']
        }
      },
      {
        path: 'transportadoras/:id',
        component: DetalhesTransportadoraComponent,
        canActivate: [AutenticacaoGuard, PossuiPerfilGuard],
        data: {
          perfis: ['Administrador', 'Sindico', 'Funcionario']
        }
      }
    ]
  },
  {
    path: '**',
    redirectTo: '/entregas'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
