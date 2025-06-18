import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface NamespaceModel {
  id?: string;
  name: string;
  parentId?: string;
}

@Injectable({
  providedIn: 'root'
})
export class NamespaceService {
  private apiUrl = `${environment.apiUrl}/Namespace`;

  constructor(private http: HttpClient) { }

  getNamespaces(): Observable<NamespaceModel[]> {
    return this.http.get<NamespaceModel[]>(this.apiUrl);
  }

  getNamespace(id: string): Observable<NamespaceModel> {
    return this.http.get<NamespaceModel>(`${this.apiUrl}/${id}`);
  }

  createNamespace(namespaceData: NamespaceModel): Observable<NamespaceModel> {
    return this.http.post<NamespaceModel>(this.apiUrl, namespaceData);
  }

  updateNamespace(id: string, namespaceData: NamespaceModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, namespaceData);
  }

  deleteNamespace(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
