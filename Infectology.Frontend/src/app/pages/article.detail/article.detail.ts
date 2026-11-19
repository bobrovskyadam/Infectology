import { Component, inject, OnInit, signal } from '@angular/core';
import { ArticleService } from '../../services/article.service';
import { ActivatedRoute } from '@angular/router';
import { Article } from '../../models/article';
import { MatCardModule } from '@angular/material/card';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';


@Component({
  imports: [MatCardModule, DatePipe, RouterLink, MatButtonModule, MatIconModule],
  selector: 'app-article.detail',
  styleUrl: './article.detail.scss',
  templateUrl: './article.detail.html',
})
export class ArticleDetail implements OnInit{
  private articleService = inject(ArticleService);
  private activatedRoute = inject(ActivatedRoute);

  article = signal<Article | null>(null);
  loading = signal(true);
  errorMessage = signal("");

  ngOnInit(): void {
    const id = Number(this.activatedRoute.snapshot.paramMap.get('id'));

    if (!Number.isInteger(id) || id <= 0) {
      this.errorMessage.set("Neplatné ID článku");
      this.loading.set(false);
      return;
    }

    this.articleService.getArticleById(id).subscribe({
      next: (data) => {
        this.article.set(data);
        this.loading.set(false);
      },
      error: (error) => {
        if (error.status === 400) {
          this.errorMessage.set("Článok sa nenašiel")
        } else {
          this.errorMessage.set('Článok sa nepodarilo načítať.')
        }
        this.loading.set(false);
      }
    })
  }
}
