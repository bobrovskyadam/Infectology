import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';

@Component({
  imports: [MatButtonModule, RouterLink],
  selector: 'app-footer',
  styleUrl: './footer.scss',
  templateUrl: './footer.html',
})
export class Footer {
  year = new Date().getFullYear();

  links = [
    { label: 'Odborné zdroje', id: 'odborne-zdroje' },
    { label: 'Podujatia a aktuality', id: 'podujatia' },
    { label: 'O spoločnosti', id: 'o-spolocnosti' },
  ];

  scrollTo(event: Event, id: string) {
    event.preventDefault();
    document.getElementById(id)?.scrollIntoView({ behavior: 'smooth' });
  }
}
