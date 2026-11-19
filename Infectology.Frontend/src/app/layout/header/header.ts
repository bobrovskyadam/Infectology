import { Component, inject } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  imports: [MatToolbarModule, MatButtonModule, RouterLink, RouterLinkActive],
  selector: 'app-header',
  styleUrl: './header.scss',
  templateUrl: './header.html',
})
export class Header {

  authService = inject(AuthService);
  private router = inject(Router);

  logout(): void {
    this.authService.logout().subscribe({
      complete: () => this.router.navigate(['/']),
      error: () => this.router.navigate(['/']),
    });
  }

  links = [
    { label: 'Odborné zdroje', id: 'odborne-zdroje' },
    { label: 'Podujatia a aktuality', id: 'podujatia' },
    { label: 'Členstvo', id: 'clenstvo' },
    { label: 'O spoločnosti', id: 'o-spolocnosti' },
  ];

  scrollTo(event: Event, id: string) {
    event.preventDefault();
    document.getElementById(id)?.scrollIntoView({ behavior: 'smooth' });
  }
}
