import { Component, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/_models/Evento';
import { EventoService } from 'src/app/_services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrl: './evento-lista.component.scss'
})
export class EventoListaComponent {
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public widthImg: number = 150;
  public marginImg: number  = 2;
  public mostrarImagem: boolean = false;
  private _filtroLista: string = "";
  public eventoId: number = 0;

  public get filtroLista() {
    return this._filtroLista
  }

  public  set filtroLista(value: string) {
    this._filtroLista = value
    this.eventosFiltrados = this._filtroLista ? this.filtarEventos(this._filtroLista) : this.eventos
  }

  public filtarEventos(filtarPor: string): Evento[] {
    filtarPor = filtarPor.toLocaleLowerCase()
    return this.eventos.filter(
        (e: Evento) => e.tema.toLocaleLowerCase().indexOf(filtarPor) !== -1 ||
          e.local.toLocaleLowerCase().indexOf(filtarPor) !== -1 
    )
  }
  
  modalRef!: BsModalRef;
  message!: string;
  
  constructor(
    private service: EventoService, 
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ){ 

  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number) {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
 
  confirm(): void {
    this.message = 'Confirmed!';
    this.modalRef.hide();
    this.spinner.show();

    this.service.delete(this.eventoId).subscribe((res: any) => {
        if (res.message === 'Deletado') {
          this.toastr.success('Excluído', 'Escluído');
        }
      }, (error: any) => {
        console.log(error);
        this.spinner.hide();
        this.toastr.error('Erro ao tentar deletar evento', 'Erro');
      }, () => {
        this.spinner.hide();    
        this.getEventos();
      });
  }
 
  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();
  }
  
  ngOnInit(): void {
     this.getEventos();
     this.spinner.show();
  }

  public getEventos() {
    this.service.getEventos().subscribe((_evento: Evento[]) => {
      this.eventos = _evento;
      this.eventosFiltrados = this.eventos
    }, (error: unknown) => {
      // console.log(error);
      this.spinner.hide();
      this.toastr.error('Erro ao carregar eventos', 'Erro');
    }, () => {
      this.spinner.hide();
    });
  }

  public alternarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }

  detalheEvento(id: number) : void {
    this.router.navigate([`/eventos/detalhe/${id}`]);
  }
}
