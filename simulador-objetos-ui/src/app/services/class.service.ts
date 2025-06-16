import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClassService {
  private apiUrl = 'http://localhost:5038/api/ClassModel';

  constructor(private http: HttpClient) { }

  getClasses(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  createClass(classModel: any): Observable<any> {
    return this.http.post(this.apiUrl, classModel);
  }
}
