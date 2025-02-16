import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService, ToastNoAnimation } from 'ngx-toastr';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public widthImg: number = 150;
  public marginImg: number  = 2;
  public mostrarImagem: boolean = false;
  private _filtroLista: string = "";

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
    private toastr: ToastrService
  ){ 

  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
 
  confirm(): void {
    this.message = 'Confirmed!';
    this.modalRef.hide();
    this.toastr.success('Hello world!', 'Toastr fun!');
  }
 
  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();
  }
  
  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos() {
    this.service.getEventos().subscribe((_evento: Evento[]) => {
      this.eventos = _evento;
      this.eventosFiltrados = this.eventos
    }, (error: unknown) => {
      console.log(error);
    });
  }

  public alternarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }



}
