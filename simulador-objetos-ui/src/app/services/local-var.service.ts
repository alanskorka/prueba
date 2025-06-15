import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocalVarModel } from '../models/local-var.model';

@Injectable({
  providedIn: 'root'
})
export class LocalVarService {
  private apiUrl = '/api/LocalVarModel';

  constructor(private http: HttpClient) { }

  getLocalVars(): Observable<LocalVarModel[]> {
    return this.http.get<LocalVarModel[]>(this.apiUrl);
  }

  getLocalVar(id: number): Observable<LocalVarModel> {
    return this.http.get<LocalVarModel>(`${this.apiUrl}/${id}`);
  }

  createLocalVar(localVarData: LocalVarModel): Observable<LocalVarModel> {
    return this.http.post<LocalVarModel>(this.apiUrl, localVarData);
  }

  updateLocalVar(id: number, localVarData: LocalVarModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, localVarData);
  }

  deleteLocalVar(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
