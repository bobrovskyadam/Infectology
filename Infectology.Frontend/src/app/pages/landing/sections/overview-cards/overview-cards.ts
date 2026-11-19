import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  imports: [MatIconModule],
  selector: 'app-overview-cards',
  styleUrl: './overview-cards.scss',
  templateUrl: './overview-cards.html',
})
export class OverviewCards {
  cards = [
    {
      icon: 'menu_book',
      title: 'Odborné zdroje',
      text: 'Overené informácie pre klinickú prax na jednom mieste.',
      items: ['Publikácie a e-zdroje', 'Sieť špecializovaných pracovísk', 'Odborné stanoviská'],
      linkLabel: 'Prejsť na zdroje',
      target: 'odborne-zdroje',
    },
    {
      icon: 'event',
      title: 'Podujatia a aktuality',
      text: 'Konferencie, odborné stretnutia a dôležité oznámenia spoločnosti.',
      items: ['Domáce a medzinárodné kongresy', 'Celoslovenská konferencia', 'Správy zo života SSI'],
      linkLabel: 'Zobraziť podujatia',
      target: 'podujatia',
    },
    {
      icon: 'groups',
      title: 'Členstvo',
      text: 'Odborná komunita lekárov v infektológii a tropickej medicíne.',
      items: ['Prístup k odbornému fóru', 'Vzdelávanie a spolupráca', 'Hlas v odborných otázkach'],
      linkLabel: 'Informácie o členstve',
      target: 'clenstvo',
    },
  ];

  scrollTo(event: Event, id: string) {
    event.preventDefault();
    document.getElementById(id)?.scrollIntoView({ behavior: 'smooth' });
  }
}
