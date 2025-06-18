import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { InterfaceModel } from '../models/interface.model';

@Injectable({
  providedIn: 'root'
})
export class InterfaceService {
  private apiUrl = `${environment.apiUrl}/InterfaceModel`;

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

  updateInterface(interfaceData: InterfaceModel): Observable<any> {
    return this.http.put(this.apiUrl, interfaceData);
  }

  deleteInterface(interfaceData: InterfaceModel): Observable<any> {
    return this.http.delete(this.apiUrl, { body: interfaceData });
  }
}
