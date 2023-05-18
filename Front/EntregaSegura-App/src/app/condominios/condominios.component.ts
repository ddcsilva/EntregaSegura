import { Component, OnInit, TemplateRef } from '@angular/core';
import { CondominioService } from '../services/condominio.service';
import { Condominio } from '../models/Condominio';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-condominios',
  templateUrl: './condominios.component.html',
  styleUrls: ['./condominios.component.scss']
})
export class CondominiosComponent implements OnInit {
  modalRef = {} as BsModalRef;

  public condominios: Condominio[] = [];
  public condominiosFiltrados: Condominio[] = [];

  private filtroAtual: string = '';

  public get filtro(): string {
    return this.filtroAtual;
  }
  public set filtro(value: string) {
    this.filtroAtual = value;
    this.condominiosFiltrados = this.filtro ? this.filtrarCondominios(this.filtro) : this.condominios;
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

  constructor(
    private condominioService: CondominioService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.getCondominios();
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
