import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface AttributeModel {
  id?: number;
  name: string;
  classId: number;
}

@Injectable({
  providedIn: 'root'
})
export class AttributeService {
  private apiUrl = `${environment.apiUrl}/AttributeModel`;

  constructor(private http: HttpClient) { }

  getAttributes(): Observable<AttributeModel[]> {
    return this.http.get<AttributeModel[]>(this.apiUrl);
  }

  getAttribute(id: number): Observable<AttributeModel> {
    return this.http.get<AttributeModel>(`${this.apiUrl}/${id}`);
  }

  createAttribute(attributeData: AttributeModel): Observable<AttributeModel> {
    return this.http.post<AttributeModel>(this.apiUrl, attributeData);
  }

  updateAttribute(id: number, attributeData: AttributeModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, attributeData);
  }

  deleteAttribute(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
