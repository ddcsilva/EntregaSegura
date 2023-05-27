import { CUSTOM_ELEMENTS_SCHEMA, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatTableModule } from '@angular/material/table';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { CdkTableModule } from '@angular/cdk/table';
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
import { CondominioService } from './services/condominio/condominio.service';
import { TratamentoErrosService } from './shared/services/tratamento-erros/tratamento-erros.service';
import { CondominiosComponent } from './components/condominios/condominios.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CondominioListaComponent } from './components/condominios/condominio-lista/condominio-lista.component';
import { CondominioDetalheComponent } from './components/condominios/condominio-detalhe/condominio-detalhe.component';
import { TituloComponent } from './shared/components/titulo/titulo.component';
import { registerLocaleData } from '@angular/common';
import ptBr from '@angular/common/locales/pt';
import { obterPaginatorIntlPortugues } from './shared/config/obter-paginator-intl-portugues';
import { ExclusaoDialogComponent } from './shared/components/exclusao-dialog/exclusao-dialog.component';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxMaskModule } from 'ngx-mask';

registerLocaleData(ptBr)

@NgModule({
  declarations: [
    AppComponent,
    CondominiosComponent,
    DashboardComponent,
    CondominioListaComponent,
    CondominioDetalheComponent,
    TituloComponent,
    ExclusaoDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    MatTableModule,
    MatPaginatorModule,
    MatIconModule,
    MatSortModule,
    CdkTableModule,
    MatButtonModule,
    MatCardModule,
    MatDividerModule,
    MatInputModule,
    MatSlideToggleModule,
    MatDialogModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatExpansionModule,
    MatMenuModule,
    MatBadgeModule,
    HttpClientModule,
    NgxSpinnerModule,
    NgxMaskModule.forRoot()
  ],
  providers: [
    { 
      provide: LOCALE_ID, 
      useValue: "pt-BR" 
    }, 
    {
      provide: MatPaginatorIntl,
      useValue: obterPaginatorIntlPortugues()
    },
    CondominioService, TratamentoErrosService],
  bootstrap: [AppComponent],
  exports: [CdkTableModule, MatTableModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
