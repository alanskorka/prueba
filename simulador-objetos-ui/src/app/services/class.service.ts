import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ClassModel } from '../models/class.model';

@Injectable({
  providedIn: 'root'
})
export class ClassService {
  private apiUrl = '/api/ClassModel';

  constructor(private http: HttpClient) { }

  getClasses(): Observable<ClassModel[]> {
    return this.http.get<ClassModel[]>(this.apiUrl);
  }

  getClass(id: number): Observable<ClassModel> {
    return this.http.get<ClassModel>(`${this.apiUrl}/${id}`);
  }

  createClass(classData: ClassModel): Observable<ClassModel> {
    return this.http.post<ClassModel>(this.apiUrl, classData);
  }

  updateClass(id: number, classData: ClassModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, classData);
  }

  deleteClass(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
