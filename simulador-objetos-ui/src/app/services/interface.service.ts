import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InterfaceModel } from '../models/interface.model';

@Injectable({
  providedIn: 'root'
})
export class InterfaceService {
  private apiUrl = '/api/InterfaceModel';

  constructor(private http: HttpClient) { }

  getInterfaces(): Observable<InterfaceModel[]> {
    return this.http.get<InterfaceModel[]>(this.apiUrl);
  }

  getInterface(id: number): Observable<InterfaceModel> {
    return this.http.get<InterfaceModel>(`${this.apiUrl}/${id}`);
  }

  createInterface(interfaceData: InterfaceModel): Observable<InterfaceModel> {
    return this.http.post<InterfaceModel>(this.apiUrl, interfaceData);
  }

  updateInterface(id: number, interfaceData: InterfaceModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, interfaceData);
  }

  deleteInterface(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
