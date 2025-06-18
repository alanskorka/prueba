import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

export interface ClassModel {
  id?: string;
  name: string;
  namespaceId?: string;
  isAbstract: boolean;
  isSealed: boolean;
  attributes: any[];
  methods: any[];
  baseClassId?: string;
  baseClass?: ClassModel;
  implementedInterfaces?: any[];
}

@Injectable({
  providedIn: 'root'
})
export class ClassService {
  private apiUrl = `${environment.apiUrl}/class`;

  constructor(private http: HttpClient) { }

  getClasses(): Observable<ClassModel[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => response?.$values ?? response)
    );
  }

  createClass(classModel: ClassModel): Observable<ClassModel> {
    return this.http.post<ClassModel>(this.apiUrl, classModel);
  }

  deleteClass(classModel: ClassModel): Observable<void> {
    return this.http.delete<void>(this.apiUrl, { body: classModel });
  }

  getClass(id: string): Observable<ClassModel> {
    return this.http.get<ClassModel>(`${this.apiUrl}/${id}`);
  }

  updateClass(classModel: ClassModel): Observable<ClassModel> {
    return this.http.put<ClassModel>(this.apiUrl, classModel);
  }
}
