import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NamespaceModel } from '../models/namespace.model';

@Injectable({
  providedIn: 'root'
})
export class NamespaceService {
  private apiUrl = '/api/Namespaces';

  constructor(private http: HttpClient) { }

  getNamespaces(): Observable<NamespaceModel[]> {
    return this.http.get<NamespaceModel[]>(this.apiUrl);
  }

  getNamespace(id: number): Observable<NamespaceModel> {
    return this.http.get<NamespaceModel>(`${this.apiUrl}/${id}`);
  }

  createNamespace(namespaceData: NamespaceModel): Observable<NamespaceModel> {
    return this.http.post<NamespaceModel>(this.apiUrl, namespaceData);
  }

  updateNamespace(id: number, namespaceData: NamespaceModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, namespaceData);
  }

  deleteNamespace(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
