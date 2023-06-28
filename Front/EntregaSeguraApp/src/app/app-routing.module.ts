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

const routes: Routes = [
  {
    path: '',
    component: CondominiosComponent
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
