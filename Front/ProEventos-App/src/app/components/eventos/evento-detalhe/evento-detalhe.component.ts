import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrl: './evento-detalhe.component.scss'
})
export class EventoDetalheComponent implements OnInit {
  public form!: FormGroup;

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
  ) { }

  ngOnInit(): void {
    this.localeService.use('pt-br');
    this.validation();
  }

  public validation() {
    this.form = this.fb.group({ 
      local: ['', [Validators.required]],
      dataEvento: ['', [Validators.required]],
      tema: ['',  [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      qtdPessoas: ['', [Validators.required, Validators.min(1), Validators.max(120000)]],
      imagemURL: ['', [Validators.required]],
      telefone: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(15)]],
      email: ['', [Validators.required, Validators.email]],
      lotes: ['', [Validators.required]],
      redesSociais: ['', [Validators.required]],
      palestrantesEventos: []
    });
  }

}
