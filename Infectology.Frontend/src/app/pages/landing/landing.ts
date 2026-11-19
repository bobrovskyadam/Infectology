import { Component } from '@angular/core';
import { Hero } from './sections/hero/hero';
import { OverviewCards } from './sections/overview-cards/overview-cards';
import { EventsNews } from './sections/events-news/events-news';
import { Resources } from './sections/resources/resources';
import { Membership } from './sections/membership/membership';

@Component({
  imports: [Hero, OverviewCards, EventsNews, Resources, Membership],
  selector: 'app-landing',
  styleUrl: './landing.scss',
  templateUrl: './landing.html',
})
export class Landing {}
