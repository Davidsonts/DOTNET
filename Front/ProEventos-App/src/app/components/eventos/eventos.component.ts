import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService, ToastNoAnimation } from 'ngx-toastr';

import { EventoService } from '../../_services/evento.service';
import { Evento } from '../../_models/Evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {

  ngOnInit(): void {
     
  }

}
