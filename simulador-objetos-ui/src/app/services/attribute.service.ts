import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

export interface AttributeModel {
  id?: string;
  name: string;
  classId: string;
  type: string;
}

@Injectable({
  providedIn: 'root'
})
export class AttributeService {
  private apiUrl = `${environment.apiUrl}/AttributeModel`;

  constructor(private http: HttpClient) { }

  getAttributes(): Observable<AttributeModel[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => response?.$values ?? [])
    );
  }

  getAttribute(id: string): Observable<AttributeModel> {
    return this.http.get<AttributeModel>(`${this.apiUrl}/${id}`);
  }

  createAttribute(attributeData: AttributeModel): Observable<AttributeModel> {
    return this.http.post<AttributeModel>(this.apiUrl, attributeData);
  }

  updateAttribute(id: string, attributeData: AttributeModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, attributeData);
  }

  deleteAttribute(attribute: AttributeModel): Observable<any> {
    return this.http.request('delete', this.apiUrl, { body: attribute });
  }
}
