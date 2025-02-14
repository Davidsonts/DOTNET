import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {
  public eventos: any = [];
  public eventosFiltrados: any = [];
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

  filtarEventos(filtarPor: string): any {
    filtarPor = filtarPor.toLocaleLowerCase()
    return this.eventos.filter(
        (e: any) => e.tema.toLocaleLowerCase().indexOf(filtarPor) !== -1 ||
          e.local.toLocaleLowerCase().indexOf(filtarPor) !== -1 
    )
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos()
  }

  public alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

   getEventos() {
    this.http.get('https://localhost:5001/api/eventos').subscribe(response => {
      this.eventos = response;
      this.eventosFiltrados = this.eventos
    }, error => {
      console.log(error);
    });
  }

}
