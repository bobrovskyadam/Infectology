import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  imports: [MatButtonModule, MatIconModule],
  selector: 'app-membership',
  styleUrl: './membership.scss',
  templateUrl: './membership.html',
})
export class Membership {
  benefits = [
    'Prístup k odbornému fóru SSI',
    'Informácie o vzdelávacích podujatiach',
    'Spolupráca s kolegami z celého Slovenska',
  ];
}
