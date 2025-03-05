import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { take } from 'rxjs/operators';

import { Lote } from '../_models/Lote';

@Injectable({
  providedIn: 'root'
})
export class LoteService {

baseUrl = 'https://localhost:5001/api/lotes';

  constructor(private http: HttpClient) { }

  public getLotesByEventoId(eventoId: number): Observable<Lote[]> {
    return this.http.get<Lote[]>(`${this.baseUrl}/${eventoId}`).pipe(take(1));
  }

  public salvarLotes(eventoId: number, lotes: Lote[]): Observable<Lote> {
    return this.http.put<Lote>(`${this.baseUrl}/${eventoId}`, lotes).pipe(take(1));
  }
  
  public delete(eventoId: number, loteId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${eventoId}/${loteId}`).pipe(take(1));
  }
}
