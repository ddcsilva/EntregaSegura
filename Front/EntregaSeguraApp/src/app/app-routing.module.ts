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
import { MoradorListaComponent } from './components/moradores/morador-lista/morador-lista.component';
import { MoradorDetalheComponent } from './components/moradores/morador-detalhe/morador-detalhe.component';
import { UsuarioComponent } from './components/usuario/usuario.component';
import { LoginComponent } from './components/usuario/login/login.component';
import { AuthGuard } from './helpers/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },

  {
    path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard]
  },
  {
    path: 'condominios', component: CondominiosComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: CondominioListaComponent },
      { path: 'detalhe', component: CondominioDetalheComponent },
      { path: 'detalhe/:id', component: CondominioDetalheComponent }
    ],
  },
  {
    path: 'transportadoras', component: TransportadorasComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: TransportadoraListaComponent },
      { path: 'detalhe', component: TransportadoraDetalheComponent },
      { path: 'detalhe/:id', component: TransportadoraDetalheComponent }
    ],
  },
  {
    path: 'unidades', component: UnidadesComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: UnidadeListaComponent },
      { path: 'detalhe', component: UnidadeDetalheComponent },
      { path: 'detalhe/:id', component: UnidadeDetalheComponent }
    ],
  },
  {
    path: 'moradores', component: MoradoresComponent, canActivate: [AuthGuard],
    children: [
      { path: '', component: MoradorListaComponent },
      { path: 'detalhe', component: MoradorDetalheComponent },
      { path: 'detalhe/:id', component: MoradorDetalheComponent }
    ],
  },
  {
    path: 'suporte', component: SuporteComponent , canActivate: [AuthGuard]
  },
  { path: '**', redirectTo: '/dashboard' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
