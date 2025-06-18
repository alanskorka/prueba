import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { LocalVarModel } from '../models/local-var.model';

@Injectable({
  providedIn: 'root'
})
export class LocalVarService {
  private apiUrl = '/api/LocalVarModel';

  constructor(private http: HttpClient) { }

  getLocalVars(): Observable<LocalVarModel[]> {
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

  getLocalVar(id: string): Observable<LocalVarModel> {
    return this.http.get<LocalVarModel>(`${this.apiUrl}/${id}`);
  }

  createLocalVar(localVarData: LocalVarModel): Observable<LocalVarModel> {
    return this.http.post<LocalVarModel>(this.apiUrl, localVarData);
  }

  updateLocalVar(id: string, localVarData: LocalVarModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, localVarData);
  }

  deleteLocalVar(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
