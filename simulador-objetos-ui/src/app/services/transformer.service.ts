import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

export interface Transformer {
  id: string;
  name: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class TransformerService {
  constructor(private apiService: ApiService) {}

  getTransformers(): Observable<Transformer[]> {
    return this.apiService.get<Transformer[]>('transformers');
  }

  transformResponse(transformerId: string, response: any): Observable<any> {
    return this.apiService.post(`transformers/${transformerId}/transform`, response);
  }
} 