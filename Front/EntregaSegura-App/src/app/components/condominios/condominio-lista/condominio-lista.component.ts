// Angular imports
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

// Model imports
import { Condominio } from '@app/models/Condominio';

// Service imports
import { CondominioService } from '@app/services/condominio.service';

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
  public id: number | undefined;
  public nome: string = '';
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
    this.carregarCondominios();
  }

  public get filtro(): string {
    return this.filtroAtual;
  }

  public set filtro(value: string) {
    this.filtroAtual = value;
    this.condominiosFiltrados = this.filtro ? this.filtrarCondominios(this.filtro) : this.condominios;
  }

  public carregarCondominios(): void {
    this.condominioService.obterTodos().subscribe({
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

  public editarCondominio(id: number): void {
    this.router.navigate(['condominios/detalhe', id]);
  }


  public abrirModal(event: any, template: TemplateRef<any>, nome: string, id: number): void {
    event.stopPropagation();
    this.id = id;
    this.nome = nome;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirmarExclusao(): void {
    this.modalRef.hide();
    this.spinner.show();

    console.log('id' + this.id);

    this.condominioService.excluir(String(this.id)).subscribe({
      next: () => {
        this.toastr.success('Condomínio excluído com sucesso!', 'Exclusão');
        this.spinner.hide();
        this.carregarCondominios();
      },
      error: (error: any) => {
        console.error(error);
        this.spinner.hide();
        this.toastr.error('Erro ao excluir o condomínio', 'Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  public negarExclusao(): void {
    this.modalRef.hide();
  }
}
