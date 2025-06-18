import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Class {
  id?: string;
  name: string;
  isAbstract: boolean;
  isSealed: boolean;
  attributes: any[];
  methods: any[];
  baseClassId?: string;
  baseClass?: Class;
  implementedInterfaces?: any[];
}

@Injectable({
  providedIn: 'root'
})
export class ClassService {
  private apiUrl = `${environment.apiUrl}/class`;

  constructor(private http: HttpClient) { }

  getClasses(): Observable<Class[]> {
    return this.http.get<Class[]>(this.apiUrl);
  }

  createClass(classModel: Class): Observable<Class> {
    return this.http.post<Class>(this.apiUrl, classModel);
  }

  deleteClass(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
