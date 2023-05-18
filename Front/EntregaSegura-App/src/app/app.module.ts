import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CondominiosComponent } from './condominios/condominios.component';
import { NavComponent } from './nav/nav.component';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';

import { FormsModule } from '@angular/forms';
import { CondominioService } from './services/condominio.service';
import { FormatCnpjPipe } from './helpers/format-cnpj.pipe';
import { FormatTelefonePipe } from './helpers/format-telefone.pipe';

@NgModule({
  declarations: [
    AppComponent,
    CondominiosComponent,
    NavComponent,
    FormatCnpjPipe,
    FormatTelefonePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [CondominioService],
  bootstrap: [AppComponent]
})
export class AppModule { }
