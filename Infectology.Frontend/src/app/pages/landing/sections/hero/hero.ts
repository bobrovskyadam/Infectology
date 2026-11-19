import { Component, computed, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { EventService } from '../../../../services/event.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { DatePipe } from '@angular/common';

@Component({
  imports: [DatePipe, MatButtonModule, MatIconModule],
  selector: 'app-hero',
  styleUrl: './hero.scss',
  templateUrl: './hero.html',
})
export class Hero {
  private eventService = inject(EventService);
  
  upcoming = toSignal(this.eventService.getUpcoming(), {initialValue: []});
  nextEvent = computed(() => this.upcoming()[0]);

  facts = [
    { label: 'Súčasť', value: 'Slovenskej lekárskej spoločnosti' },
    { label: 'Sieť', value: '4 typy pracovísk' },
    { label: 'Pre členov', value: 'Odborné fórum SSI' },
  ];

  scrollTo(id: string) {
    document.getElementById(id)?.scrollIntoView({ behavior: 'smooth' });
  }
}
