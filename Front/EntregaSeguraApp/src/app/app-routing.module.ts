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

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'condominios', canActivate: [AuthGuard],
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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
