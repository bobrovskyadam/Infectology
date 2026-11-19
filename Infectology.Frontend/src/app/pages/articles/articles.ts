import { Component, OnInit, inject, signal, ChangeDetectionStrategy } from '@angular/core';
import { ArticleService } from '../../services/article.service';
import { Article } from '../../models/article';
import { DatePipe, SlicePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-articles',
  imports: [DatePipe, SlicePipe, MatCardModule, MatButtonModule, RouterLink],
  templateUrl: './articles.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './articles.scss',
})

export class Articles implements OnInit {
  private articleService = inject(ArticleService);

  articles = signal<Article[]>([]);
  loading = signal(true);
  errorMessage = signal('');

  ngOnInit(): void {
    this.articleService.getArticles().subscribe({
      next: (data) => {
        this.articles.set(data);
        this.loading.set(false);
      },
      error: (error) => {
        this.errorMessage.set('Články sa nepodarilo načítať.');
        this.loading.set(false);
      },
    });
  }
}
