// Angular imports
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
import { PerfilComponent } from './components/usuarios/perfil/perfil.component';

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
import { CondominioListaComponent } from './components/condominios/condominio-lista/condominio-lista.component';
import { CondominioDetalheComponent } from './components/condominios/condominio-detalhe/condominio-detalhe.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { LoginComponent } from './components/usuarios/login/login.component';
import { RegistroComponent } from './components/usuarios/registro/registro.component';

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
    DashboardComponent,
    CondominioListaComponent,
    CondominioDetalheComponent,
    UsuariosComponent,
    LoginComponent,
    RegistroComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
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
