import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from 'src/app/_models/Evento';
import { EventoService } from 'src/app/_services/evento.service';
@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrl: './evento-detalhe.component.scss'
})
export class EventoDetalheComponent implements OnInit {
  public form!: FormGroup;
  evento = {} as Evento;
  _state: string = 'post';

  get f(): any {
    return this.form.controls;
  }
  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY HH:mm a',      
      containerClass: 'theme-default',
      showWeekNumbers: false
    };
  }

  constructor(private fb: FormBuilder,
    private localeService: BsLocaleService,
    private router: ActivatedRoute,
    private eventoService: EventoService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.localeService.use('pt-br');
    this.fecthEvento();
    this.validation();
  }

  public validation() {
    this.form = this.fb.group({ 
      local: ['', [Validators.required]],
      dataEvento: ['', [Validators.required]],
      tema: ['',  [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      qtdPessoas: ['', [Validators.required, Validators.min(1), Validators.max(120000)]],
      imageUrl: ['', [Validators.required]],
      telefone: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(15)]],
      email: ['', [Validators.required, Validators.email]],
      // lotes: ['', [Validators.required]],
      // redesSociais: ['', [Validators.required]],
      // palestrantesEventos: []
    });
  }

  fecthEvento(): void {
    const eventoIdParam = this.router.snapshot.paramMap.get('id');
    if (eventoIdParam !== null) {
      this._state = 'put';
      this.spinner.show();
      this.eventoService.getEventoById(+eventoIdParam).subscribe(
        (evento: any) => {
          this.evento = { ... evento };
          this.form.patchValue(evento);
        }, (error: any) => {
          this.toastr.error('Erro ao tentar carregar evento.', 'Erro!');
        }, () => { }
      ).add(() => { this.spinner.hide(); });
    }
  }

  submit(): void {
    this.spinner.show();
    this._state === 'post' ? 
      this.evento = { ... this.form.value } : 
      this.evento = { id: this.evento.id, ... this.form.value };

    if (this.form.valid) {
      (this.eventoService as any)[this._state](this.evento).subscribe(
        (evento: any) => {
          this.toastr.success('Evento salvo com sucesso!', 'Sucesso!');
        }, (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar salvar evento.', 'Erro!');
        }, () => {
          this.spinner.hide();
        }
      );
    }
  }

  public resetForm(): void {
    this.form.reset();
  }
}
