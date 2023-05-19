// Angular imports
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

// Components imports
import { AppComponent } from './app.component';
import { NavComponent } from './shared/nav/nav.component';
import { TituloComponent } from './shared/titulo/titulo.component';
import { CondominiosComponent } from './components/condominios/condominios.component';
import { UnidadesComponent } from './components/unidades/unidades.component';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { MoradoresComponent } from './components/moradores/moradores.component';
import { EntregasComponent } from './components/entregas/entregas.component';
import { TransportadorasComponent } from './components/transportadoras/transportadoras.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { PerfilComponent } from './components/perfil/perfil.component';

// Service imports
import { CondominioService } from './services/condominio.service';

// Pipe imports
import { FormatCnpjPipe } from './helpers/format-cnpj.pipe';
import { FormatTelefonePipe } from './helpers/format-telefone.pipe';

// Module imports
import { AppRoutingModule } from './app-routing.module';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { DashboardComponent } from './components/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    TituloComponent,
    CondominiosComponent,
    UnidadesComponent,
    FuncionariosComponent,
    MoradoresComponent,
    EntregasComponent,
    TransportadorasComponent,
    ContatosComponent,
    PerfilComponent,
    FormatCnpjPipe,
    FormatTelefonePipe,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot(
      {
        timeOut: 3000,
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,
        progressBar: true
      }
    ),
    NgxSpinnerModule
  ],
  providers: [CondominioService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
