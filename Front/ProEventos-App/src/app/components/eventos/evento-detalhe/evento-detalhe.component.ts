import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, AbstractControlDirective, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from 'src/app/_models/Evento';
import { Lote } from 'src/app/_models/Lote';
import { EventoService } from 'src/app/_services/evento.service';
import { LoteService } from 'src/app/_services/lote.service';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrl: './evento-detalhe.component.scss'
})
export class EventoDetalheComponent implements OnInit {
  modalRef: BsModalRef | null = null;
  public form!: FormGroup;
  evento = {} as Evento;
  _state: string = 'post';
  eventoId: number = 0;
  loteAtual: { id: number; nome: string; indice: number} = { id: 0, nome: '', indice: 0 };
  lotesData: any[] = [];
  file!: File;
  imagemURL = 'assets/img/upload.png';

  get modoEditar(): boolean {
    return this._state === 'put';
  }

  get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }

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

  get bsConfigLote(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',      
      containerClass: 'theme-default',
      showWeekNumbers: false
    };
  }

  constructor(private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRoute: ActivatedRoute,
    private eventoService: EventoService,
    private loteService: LoteService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private modalService: BsModalService
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
      imageUrl: [''],
      telefone: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(15)]],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.fb.array([]),
      // redesSociais: ['', [Validators.required]],
      // palestrantesEventos: []
    });
  }

  addicionrLote(): void {
   // (this.form.get('lotes') as FormArray).push(this.criarLote(lote));
   this.lotes.push(this.criarLote({id: 0} as Lote));
  }

  criarLote(lote: Lote): FormGroup {  
    let dataInicio = lote?.dataInicio ? new Date(lote.dataInicio) : null;
    if (dataInicio && isNaN(dataInicio.getTime())) {
      console.warn('Invalid date detected:', lote.dataInicio);
      dataInicio = null;
    }

    let dataFim = lote?.dataFim ? new Date(lote.dataFim) : null;
    if (dataFim && isNaN(dataFim.getTime())) {
      console.warn('Invalid date detected:', lote.dataFim);
      dataFim = null;
    }

    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome, [Validators.required]],
      quantidade: [lote.quantidade, [Validators.required]],
      preco: [lote.preco, [Validators.required]],
      dataInicio: [dataInicio, [Validators.required]],
      dataFim: [dataFim, [Validators.required]],
      eventoId: [this.eventoId],
    })
  }

  fecthEvento(): void {
    this.eventoId = +this.activatedRoute.snapshot.paramMap.get('id')!;

    if (this.eventoId !== null && this.eventoId !== 0) {
      this._state = 'put';
      this.spinner.show();
      // (this.form.get('lotes') as FormArray).clear();
      this.eventoService.getEventoById(this.eventoId).subscribe(
        (evento: any) => {
          this.evento = { ... evento, dataEvento: evento.dataEvento ? new Date(evento.dataEvento) : null };
          // this.form.patchValue(evento);
          this.form.patchValue({
            ...evento,
            dataEvento: evento.dataEvento ? new Date(evento.dataEvento) : null
          });
          // this.evento.lotes.forEach((lote: Lote) => {
          //   this.lotes.push(this.criarLote(lote));
          // });
          if(evento.imageUrl !== '') {
            this.imagemURL = environment.apiURL + 'resources/images/' + evento.imageUrl;
          }
          this.carregarLotes();
        }, (error: any) => {
          this.toastr.error('Erro ao tentar carregar evento.', 'Erro!');
        }, () => { }
      ).add(() => { this.spinner.hide(); });
    }
  }

  carregarLotes(): void {
    this.loteService.getLotesByEventoId(this.eventoId).subscribe(
      (lotesRetorno: any[]) => {
        this.lotes.clear(); // Clear existing controls
        lotesRetorno.forEach((lote) => {
          this.lotes.push(this.criarLote(lote));
        });
      },
      (error: any) => {
        this.toastr.error('Erro ao tentar carregar lotes', 'Erro');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  submit(): void {
    this.spinner.show();
    this._state === 'post' ? 
      this.evento = { ... this.form.value } : 
      this.evento = { id: this.evento.id, ... this.form.value };

    if (this.form.valid) {
      (this.eventoService as any)[this._state](this.evento).subscribe(
        (evento: Evento) => {
          this.toastr.success('Evento salvo com sucesso!', 'Sucesso!');
          this.router.navigate([`/eventos/detalhe/${evento.id}`]);
        }, (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar salvar evento.', 'Erro!');
        }, () => {
          this.spinner.hide();
        }
      );
    }
  }

  public salvarLotes(): void {
    this.spinner.show();
    if (this.form.controls.lotes.valid) {
      this.loteService.salvarLotes(this.eventoId, this.form.value.lotes).subscribe(
        () => {
          this.toastr.success('Lotes salvos com sucesso!', 'Sucesso!');
          this.fecthEvento();
        },
        (error: any) => {
          this.toastr.error('Erro ao tentar salvar lotes.', 'Erro!');
          console.error(error);
        }, () => { }
      ).add(() => { this.spinner.hide(); });
    }
  }

  public resetForm(): void {
    this.form.reset();
  }

  cssValidator(campoForm: FormControl | AbstractControl | null): any {
    if (!campoForm) {
      return {}; // Return an empty object or default class if campoForm is null
    }
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  removeLote(template: TemplateRef<any>, indice: number): void {
    this.loteAtual.id = this.lotes.at(indice).get('id')?.value;
    this.loteAtual.nome = this.lotes.at(indice).get('nome')?.value;
    this.loteAtual.indice = indice;

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmDeleteLote(): void {
    this.modalRef?.hide();
    this.spinner.show();
    this.loteService.delete(this.eventoId, this.loteAtual.id).subscribe(
      () => {
        this.toastr.success('Lote deletado com sucesso!', 'Sucesso!');
        this.lotes.removeAt(this.loteAtual.indice);
      },
      (error: any) => {
        this.toastr.error('Erro ao tentar deletar lote.', 'Erro!');
        console.error(error);
      }, () => { }
    ).add(() => { this.spinner.hide(); });
  }

  declineDeleteLote(): void {
    this.toastr.info('Operação cancelada.', 'Informação!');
    this.modalRef?.hide();
  }

  mudarValorData(value: Date, indice: number, campo: string): void {
    const control = this.lotes.at(indice).get(campo);
    if (control) {
      if (value && !isNaN(value.getTime())) {
        control.setValue(value);
      } else {
        console.error('Invalid date selected:', value);
        control.setValue(null);
      }
    }
  } 

  public retornaTituloLote(nome: string): string {
    return nome === null || nome === '' ? 'Nome do lote' : nome;
  }

onFileChange(ev: Event): void {  
  const input = ev.target as HTMLInputElement;

  if (input.files && input.files.length > 0) {
    this.file = input.files[0]; // Assign only the first file correctly
    const reader = new FileReader();

    reader.onload = (event: ProgressEvent<FileReader>) => {
      this.imagemURL = event.target?.result as string; // Ensure type safety
    };

    reader.readAsDataURL(this.file);
    this.uploadImagem();
  }
}

  uploadImagem(): void {  
    this.spinner.show();
    this.eventoService.uploadImagem(this.eventoId, this.file).subscribe(
      () => {
        this.toastr.success('Imagem atualizada com sucesso!', 'Sucesso!');
        this.fecthEvento();
      },
      (error: any) => {
        this.toastr.error('Erro ao tentar atualizar imagem.', 'Erro!');
        console.error(error);
      }, () => { }
    ).add(() => { this.spinner.hide(); });
  }
}
