import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { InterfaceMethodModel } from '../models/interface-method.model';

@Injectable({
  providedIn: 'root'
})
export class InterfaceMethodService {
  private apiUrl = '/api/InterfaceMethodModel';

  constructor(private http: HttpClient) { }

  getInterfaceMethods(): Observable<InterfaceMethodModel[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => response.$values || response)
    );
  }

  getInterfaceMethod(id: string): Observable<InterfaceMethodModel> {
    return this.http.get<InterfaceMethodModel>(`${this.apiUrl}/${id}`);
  }

  createInterfaceMethod(interfaceMethodData: InterfaceMethodModel): Observable<InterfaceMethodModel> {
    return this.http.post<InterfaceMethodModel>(this.apiUrl, interfaceMethodData);
  }

  updateInterfaceMethod(id: string, interfaceMethodData: InterfaceMethodModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, interfaceMethodData);
  }

  deleteInterfaceMethod(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
