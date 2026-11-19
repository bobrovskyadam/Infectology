import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Article } from '../models/article';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  private http = inject(HttpClient);

  private apiUrl = environment.apiUrl;

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(`${this.apiUrl}/Article`);
  }

  getArticleById(id: number): Observable<Article> {
    return this.http.get<Article>(`${this.apiUrl}/Article/${id}`);
  }
}
