// Angular core imports
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA, LOCALE_ID, NgModule } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import ptBr from '@angular/common/locales/pt';

// Angular material imports
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatMenuModule } from '@angular/material/menu';
import { MatBadgeModule } from '@angular/material/badge';
import { MatSelectModule } from '@angular/material/select';
import { CdkTableModule } from '@angular/cdk/table';

// Angular custom imports
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Custom component imports
import { CondominiosComponent } from './components/condominios/condominios.component';
import { CondominioListaComponent } from './components/condominios/condominio-lista/condominio-lista.component';
import { CondominioDetalheComponent } from './components/condominios/condominio-detalhe/condominio-detalhe.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TituloComponent } from './shared/components/titulo/titulo.component';
import { ExclusaoDialogComponent } from './shared/components/exclusao-dialog/exclusao-dialog.component';
import { TransportadorasComponent } from './components/transportadoras/transportadoras.component';
import { TransportadoraListaComponent } from './components/transportadoras/transportadora-lista/transportadora-lista.component';
import { TransportadoraDetalheComponent } from './components/transportadoras/transportadora-detalhe/transportadora-detalhe.component';
import { UnidadesComponent } from './components/unidades/unidades.component';
import { UnidadeListaComponent } from './components/unidades/unidade-lista/unidade-lista.component';
import { UnidadeDetalheComponent } from './components/unidades/unidade-detalhe/unidade-detalhe.component';
import { SuporteComponent } from './components/suporte/suporte.component';

// Custom services imports
import { CepService } from './shared/services/cep/cep.service';
import { CondominioService } from './services/condominio/condominio.service';
import { TransportadoraService } from './services/transportadora/transportadora.service';
import { TratamentoErrosService } from './shared/services/tratamento-erros/tratamento-erros.service';
import { obterPaginatorIntlPortugues } from './shared/config/obter-paginator-intl-portugues';

// Third-party library imports
import { HttpClientModule } from '@angular/common/http';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxMaskModule } from 'ngx-mask';
import { ToastrModule } from 'ngx-toastr';
import { ConfirmacaoDialogComponent } from './shared/components/confirmacao-dialog/confirmacao-dialog.component';
import { UnidadeService } from './services/unidade/unidade.service';
import { FormatarTelefonePipe } from './shared/pipes/formatar-telefone.pipe';

registerLocaleData(ptBr)

@NgModule({
  declarations: [
    // Components
    AppComponent,
    CondominioDetalheComponent,
    CondominioListaComponent,
    CondominiosComponent,
    DashboardComponent,
    ExclusaoDialogComponent,
    TituloComponent,
    TransportadoraDetalheComponent,
    TransportadoraListaComponent,
    TransportadorasComponent,
    UnidadeDetalheComponent,
    UnidadeListaComponent,
    UnidadesComponent,
    SuporteComponent,
    ConfirmacaoDialogComponent,
    FormatarTelefonePipe
  ],
  imports: [
    // Angular modules
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,

    // Third party modules
    AppRoutingModule,
    HttpClientModule,
    NgxMaskModule.forRoot(),
    NgxSpinnerModule,
    ToastrModule.forRoot(),

    // Angular Material modules
    CdkTableModule,
    MatButtonModule,
    MatBadgeModule,
    MatCardModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatPaginatorModule,
    MatSidenavModule,
    MatSlideToggleModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule,
    MatSelectModule
  ],
  providers: [
    // Angular providers
    {
      provide: LOCALE_ID,
      useValue: "pt-BR"
    },
    {
      provide: MatPaginatorIntl,
      useValue: obterPaginatorIntlPortugues()
    },

    // Custom providers
    CepService,
    CondominioService,
    UnidadeService,
    TransportadoraService,
    TratamentoErrosService
  ],
  bootstrap: [AppComponent],
  exports: [
    // Exported modules
    CdkTableModule,
    MatTableModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
