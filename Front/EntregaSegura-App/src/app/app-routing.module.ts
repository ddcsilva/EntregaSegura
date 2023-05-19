import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CondominiosComponent } from './components/condominios/condominios.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EntregasComponent } from './components/entregas/entregas.component';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { MoradoresComponent } from './components/moradores/moradores.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { TransportadorasComponent } from './components/transportadoras/transportadoras.component';
import { UnidadesComponent } from './components/unidades/unidades.component';

const routes: Routes = [
  { path: 'condominios', component: CondominiosComponent},
  { path: 'contatos', component: ContatosComponent},
  { path: 'dashboard', component: DashboardComponent},
  { path: 'entregas', component: EntregasComponent},
  { path: 'funcionarios', component: FuncionariosComponent},
  { path: 'moradores', component: MoradoresComponent},
  { path: 'perfil', component: PerfilComponent},
  { path: 'transportadoras', component: TransportadorasComponent},
  { path: 'unidades', component: UnidadesComponent},
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: '/dashboard', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
