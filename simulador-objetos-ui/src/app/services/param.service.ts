import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ParamModel } from '../models/param.model';

@Injectable({
  providedIn: 'root'
})
export class ParamService {
  private apiUrl = '/api/ParamModel';

  constructor(private http: HttpClient) { }

  getParams(): Observable<ParamModel[]> {
    return this.http.get<ParamModel[]>(this.apiUrl);
  }

  getParam(id: number): Observable<ParamModel> {
    return this.http.get<ParamModel>(`${this.apiUrl}/${id}`);
  }

  createParam(paramData: ParamModel): Observable<ParamModel> {
    return this.http.post<ParamModel>(this.apiUrl, paramData);
  }

  updateParam(id: number, paramData: ParamModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, paramData);
  }

  deleteParam(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
