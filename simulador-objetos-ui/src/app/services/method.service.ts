import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MethodService {
  private apiUrl = '/api/MethodModel';

  constructor(private http: HttpClient) { }

  getMethods(): Observable<MethodModel[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => {
        console.log('Raw response from backend:', response);
        // Handle the $values format from backend
        if (response && response.$values) {
          return response.$values;
        }
        return response;
      })
    );
  }

  getMethod(id: string): Observable<MethodModel> {
    return this.http.get<MethodModel>(`${this.apiUrl}/${id}`);
  }

  createMethod(methodData: MethodModel): Observable<MethodModel> {
    return this.http.post<MethodModel>(this.apiUrl, methodData);
  }

  updateMethod(id: string, methodData: MethodModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, methodData);
  }

  deleteMethod(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

export interface MethodModel {
  id?: string;
  name: string;
  returnType: string;
  parameters?: any[];
  localVars?: any[];
  methodCalls?: any[];
  isVirtual?: boolean;
  isStatic?: boolean;
  isOverride?: boolean;
  classId: string;
}
