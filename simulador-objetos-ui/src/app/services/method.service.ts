import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MethodModel } from '../models/method.model';

@Injectable({
  providedIn: 'root'
})
export class MethodService {
  private apiUrl = '/api/MethodModel';

  constructor(private http: HttpClient) { }

  getMethods(): Observable<MethodModel[]> {
    return this.http.get<MethodModel[]>(this.apiUrl);
  }

  getMethod(id: number): Observable<MethodModel> {
    return this.http.get<MethodModel>(`${this.apiUrl}/${id}`);
  }

  createMethod(methodData: MethodModel): Observable<MethodModel> {
    return this.http.post<MethodModel>(this.apiUrl, methodData);
  }

  updateMethod(id: number, methodData: MethodModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, methodData);
  }

  deleteMethod(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
