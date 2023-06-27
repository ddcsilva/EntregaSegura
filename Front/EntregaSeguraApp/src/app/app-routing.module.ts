import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CondominiosComponent } from './components/condominios/condominios.component';
import { DetalhesCondominioComponent } from './components/condominios/detalhes-condominio/detalhes-condominio.component';

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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
