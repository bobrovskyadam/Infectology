import { Component, computed, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { EventService } from '../../../../services/event.service';
import { DatePipe } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  imports: [MatIconModule, DatePipe],
  selector: 'app-events-news',
  styleUrl: './events-news.scss',
  templateUrl: './events-news.html',
})
export class EventsNews {
  private eventService = inject(EventService);

  upcoming = toSignal(this.eventService.getUpcoming(), { initialValue: [] });
  featured = computed(() => this.upcoming()[0]);
  others = computed(() => this.upcoming().slice(1, 4));

  news = [
    {
      tag: 'Zo spoločnosti',
      title: 'Poďakovanie účastníkom 18. celoslovenskej infektologickej konferencie v Nitre',
      url: '#',
    },
    {
      tag: 'Stanovisko',
      title: 'Vyjadrenie SSI k výrokom splnomocnenca vlády na prešetrenie pandémie COVID-19',
      url: '#',
    },
  ];
}
