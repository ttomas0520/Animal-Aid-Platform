import { Injectable } from '@angular/core';
import { CategoryModel } from '../models/category-model';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private serverUrl = 'https://localhost:7022';
  constructor(private http: HttpClient) {}

  addCategory(model: CategoryModel): Observable<CategoryModel> {
    const url = `${this.serverUrl}/api/categories`;
    return this.http.post<CategoryModel>(url, model);
  }

  deleteCategory(category: CategoryModel): Observable<unknown> {
    const url = `${this.serverUrl}/api/categories`;
    const params = new HttpParams()
      .set('name', category.name)
      .set('url', category.urlHandle);
    return this.http.delete(`https://localhost:7022/api/categories`, {
      params,
    });
  }

  getAllCategory(): Observable<CategoryModel[]> {
    const url = `${this.serverUrl}/api/categories`;
    return this.http.get<CategoryModel[]>(url);
  }
}
