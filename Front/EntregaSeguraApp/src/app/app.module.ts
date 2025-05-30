import { CUSTOM_ELEMENTS_SCHEMA, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import ptBr from '@angular/common/locales/pt';
// Material Form Controls
import { MatLegacyAutocompleteModule as MatAutocompleteModule } from '@angular/material/legacy-autocomplete';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacySliderModule as MatSliderModule } from '@angular/material/legacy-slider';
import { MatLegacySlideToggleModule as MatSlideToggleModule } from '@angular/material/legacy-slide-toggle';
// Material Navigation
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
// Material Layout
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatLegacyListModule as MatListModule } from '@angular/material/legacy-list';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatTreeModule } from '@angular/material/tree';
// Material Buttons & Indicators
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatBadgeModule } from '@angular/material/badge';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';
import { MatLegacyProgressBarModule as MatProgressBarModule } from '@angular/material/legacy-progress-bar';
import { MatRippleModule } from '@angular/material/core';
// Material Popups & Modals
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatLegacySnackBarModule as MatSnackBarModule } from '@angular/material/legacy-snack-bar';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
// Material Data tables
import { MatLegacyPaginatorIntl as MatPaginatorIntl, MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { CondominiosComponent } from './components/condominios/condominios.component';
import { NgxMaskModule } from 'ngx-mask';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { FormatarCnpjPipe } from './shared/helpers/formatar-cnpj.pipe';
import { FormatarTelefonePipe } from './shared/helpers/formatar-telefone.pipe';
import { ExclusaoDialogComponent } from './shared/components/exclusao-dialog/exclusao-dialog.component';
import { DetalhesCondominioComponent } from './components/condominios/detalhes-condominio/detalhes-condominio.component';
import { TituloComponent } from './shared/components/titulo/titulo.component';
import { ConfirmacaoDialogComponent } from './shared/components/confirmacao-dialog/confirmacao-dialog.component';
import { obterPaginatorIntlPortugues } from './shared/config/obter-paginator-intl-portugues';
import { TransportadorasComponent } from './components/transportadoras/transportadoras.component';
import { DetalhesTransportadoraComponent } from './components/transportadoras/detalhes-transportadora/detalhes-transportadora.component';
import { UnidadesComponent } from './components/unidades/unidades.component';
import { DetalhesUnidadeComponent } from './components/unidades/detalhes-unidade/detalhes-unidade.component';
import { MoradoresComponent } from './components/moradores/moradores.component';
import { DetalhesMoradorComponent } from './components/moradores/detalhes-morador/detalhes-morador.component';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { DetalhesFuncionarioComponent } from './components/funcionarios/detalhes-funcionario/detalhes-funcionario.component';
import { registerLocaleData } from '@angular/common';
import { MainComponent } from './components/main/main.component';
import { EntregasComponent } from './components/entregas/entregas.component';
import { DetalhesEntregaComponent } from './components/entregas/detalhes-entrega/detalhes-entrega.component';
import { LoginComponent } from './components/login/login.component';
import { TokenInterceptor } from './helpers/interceptors/token.interceptor';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LayoutModule } from '@angular/cdk/layout';

registerLocaleData(ptBr)

@NgModule({ declarations: [
        AppComponent,
        FormatarTelefonePipe,
        FormatarCnpjPipe,
        CondominiosComponent,
        ExclusaoDialogComponent,
        DetalhesCondominioComponent,
        TituloComponent,
        ConfirmacaoDialogComponent,
        TransportadorasComponent,
        DetalhesTransportadoraComponent,
        UnidadesComponent,
        DetalhesUnidadeComponent,
        MoradoresComponent,
        DetalhesMoradorComponent,
        FuncionariosComponent,
        DetalhesFuncionarioComponent,
        MainComponent,
        EntregasComponent,
        DetalhesEntregaComponent,
        LoginComponent,
        DashboardComponent
    ],
    schemas: [
        CUSTOM_ELEMENTS_SCHEMA
    ],
    bootstrap: [AppComponent], imports: [
        // Angular Modules
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        // Angular Material Modules
        MatAutocompleteModule,
        MatCheckboxModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatFormFieldModule,
        MatInputModule,
        MatRadioModule,
        MatSelectModule,
        MatSliderModule,
        MatSlideToggleModule,
        MatMenuModule,
        MatSidenavModule,
        MatToolbarModule,
        MatCardModule,
        MatDividerModule,
        MatExpansionModule,
        MatGridListModule,
        MatListModule,
        MatStepperModule,
        MatTabsModule,
        MatTreeModule,
        MatButtonModule,
        MatButtonToggleModule,
        MatBadgeModule,
        MatChipsModule,
        MatIconModule,
        MatProgressSpinnerModule,
        MatProgressBarModule,
        MatRippleModule,
        MatBottomSheetModule,
        MatDialogModule,
        MatSnackBarModule,
        MatTooltipModule,
        MatPaginatorModule,
        MatSortModule,
        MatTableModule,
        // Third Party Modules
        NgxMaskModule.forRoot(),
        NgxSpinnerModule,
        ToastrModule.forRoot(),
        LayoutModule], providers: [
        {
            provide: LOCALE_ID,
            useValue: "pt-BR"
        },
        {
            provide: MatPaginatorIntl,
            useValue: obterPaginatorIntlPortugues()
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true
        },
        provideHttpClient(withInterceptorsFromDi())
    ] })
export class AppModule { }
