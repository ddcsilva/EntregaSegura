import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CondominiosComponent } from './components/condominios/condominios.component';

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
    component: CondominiosComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
