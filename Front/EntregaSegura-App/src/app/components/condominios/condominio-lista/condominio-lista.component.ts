// Angular imports
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

// Model imports
import { Condominio } from 'src/app/models/Condominio';

// Service imports
import { CondominioService } from 'src/app/services/condominio.service';

// Library imports
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-condominio-lista',
  templateUrl: './condominio-lista.component.html',
  styleUrls: ['./condominio-lista.component.scss']
})
export class CondominioListaComponent implements OnInit {

  public get condominioService(): CondominioService {
    return this._condominioService;
  }
  
  public set condominioService(value: CondominioService) {
    this._condominioService = value;
  }

  public condominios: Condominio[] = [];
  public condominiosFiltrados: Condominio[] = [];
  private filtroAtual: string = '';
  modalRef = {} as BsModalRef;

  constructor(
    private router: Router,
    private _condominioService: CondominioService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.getCondominios();
  }

  public get filtro(): string {
    return this.filtroAtual;
  }

  public set filtro(value: string) {
    this.filtroAtual = value;
    this.condominiosFiltrados = this.filtro ? this.filtrarCondominios(this.filtro) : this.condominios;
  }

  public getCondominios(): void {
    this.condominioService.getCondominios().subscribe({
      next: (dadosCondominios: Condominio[]) => {
        this.condominios = dadosCondominios;
        this.condominiosFiltrados = this.condominios;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar os condomínios', 'Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  public filtrarCondominios(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.condominios.filter(
      (condominio: { cnpj: string; nome: string; cidade: string }) =>
        condominio.cnpj.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        condominio.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        condominio.cidade.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  public editarCondominio(id: string): void {
    this.router.navigate(['condominios/detalhe', id]);
  }
  

  public abrirModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirmarExclusao(): void {
    this.modalRef.hide();
    this.toastr.success('Condomínio excluído com sucesso!', 'Exclusão');
  }

  public negarExclusao(): void {
    this.modalRef.hide();
  }
}
