import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CondominiosComponent } from './components/condominios/condominios.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CondominioListaComponent } from './components/condominios/condominio-lista/condominio-lista.component';
import { CondominioDetalheComponent } from './components/condominios/condominio-detalhe/condominio-detalhe.component';
import { TransportadorasComponent } from './components/transportadoras/transportadoras.component';
import { TransportadoraListaComponent } from './components/transportadoras/transportadora-lista/transportadora-lista.component';
import { TransportadoraDetalheComponent } from './components/transportadoras/transportadora-detalhe/transportadora-detalhe.component';
import { UnidadesComponent } from './components/unidades/unidades.component';
import { UnidadeListaComponent } from './components/unidades/unidade-lista/unidade-lista.component';
import { UnidadeDetalheComponent } from './components/unidades/unidade-detalhe/unidade-detalhe.component';
import { SuporteComponent } from './components/suporte/suporte.component';
import { MoradoresComponent } from './components/moradores/moradores.component';
import { MoradoresListaComponent } from './components/moradores/moradores-lista/moradores-lista.component';

const routes: Routes = [
  {
    path: 'dashboard', component: DashboardComponent
  },
  {
    path: 'condominios', component: CondominiosComponent,
    children: [
      { path: '', component: CondominioListaComponent },
      { path: 'detalhe', component: CondominioDetalheComponent },
      { path: 'detalhe/:id', component: CondominioDetalheComponent }
    ],
  },
  {
    path: 'transportadoras', component: TransportadorasComponent,
    children: [
      { path: '', component: TransportadoraListaComponent },
      { path: 'detalhe', component: TransportadoraDetalheComponent },
      { path: 'detalhe/:id', component: TransportadoraDetalheComponent }
    ],
  },
  {
    path: 'unidades', component: UnidadesComponent,
    children: [
      { path: '', component: UnidadeListaComponent },
      { path: 'detalhe', component: UnidadeDetalheComponent },
      { path: 'detalhe/:id', component: UnidadeDetalheComponent }
    ],
  },
  {
    path: 'moradores', component: MoradoresComponent,
    children: [
      { path: '', component: MoradoresListaComponent },
      { path: 'detalhe', component: UnidadeDetalheComponent },
      { path: 'detalhe/:id', component: UnidadeDetalheComponent }
    ],
  },
  {
    path: 'suporte', component: SuporteComponent
  },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: '/dashboard', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
