import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { MethodCallModel } from '../models/method-call.model';

@Injectable({
  providedIn: 'root'
})
export class MethodCallService {
  private apiUrl = '/api/MethodCallModel';

  constructor(private http: HttpClient) { }

  getMethodCalls(): Observable<MethodCallModel[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => response.$values || response)
    );
  }

  getMethodCall(id: string): Observable<MethodCallModel> {
    return this.http.get<MethodCallModel>(`${this.apiUrl}/${id}`);
  }

  createMethodCall(methodCallData: MethodCallModel): Observable<MethodCallModel> {
    return this.http.post<MethodCallModel>(this.apiUrl, methodCallData);
  }

  updateMethodCall(id: string, methodCallData: MethodCallModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, methodCallData);
  }

  deleteMethodCall(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
