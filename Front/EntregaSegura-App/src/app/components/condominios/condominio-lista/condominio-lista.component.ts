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
  public condominios: Condominio[] = [];
  public condominiosFiltrados: Condominio[] = [];
  public id: number | undefined;
  public nome: string = '';
  public filtroAtual: string = '';
  modalRef = {} as BsModalRef;

  constructor(
    private router: Router,
    private condominioService: CondominioService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.carregarCondominios();
  }

  public aplicarFiltro(value: string): void {
    this.filtroAtual = value;
    this.condominiosFiltrados = this.filtroAtual ? this.filtrarCondominios(this.filtroAtual) : this.condominios;
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

  public filtrarCondominios(filtrarPor: string): Condominio[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.condominios.filter(
      (condominio: Condominio) =>
        condominio.cnpj.toLocaleLowerCase().includes(filtrarPor) ||
        condominio.nome.toLocaleLowerCase().includes(filtrarPor) ||
        condominio.cidade.toLocaleLowerCase().includes(filtrarPor)
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

    this.condominioService.excluir(String(this.id)).subscribe({
      next: () => {
        this.toastr.success('Condomínio excluído com sucesso!', 'Exclusão');
        this.carregarCondominios();
      },
      error: (error: any) => {
        this.spinner.hide();
        if (Array.isArray(error)) {
          error.forEach((mensagemErro: string) => this.toastr.error(mensagemErro, 'Erro de Validação!'));
        } else {
          this.toastr.error(error, 'Erro!');
        }
      }      
    }).add(() => this.spinner.hide());
  }

  public negarExclusao(): void {
    this.modalRef.hide();
  }
}
