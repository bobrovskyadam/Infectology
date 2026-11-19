import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';

@Component({
  imports: [MatIconModule, RouterLink],
  selector: 'app-resources',
  styleUrl: './resources.scss',
  templateUrl: './resources.html',
})
export class Resources {
  columns = [
    {
      icon: 'local_library',
      title: 'Publikácie a stanoviská',
      text: 'Odborná literatúra, prístup k vedeckým databázam a oficiálne stanoviská spoločnosti.',
      links: [
        { label: 'Bezplatný prístup k e-zdrojom cez CVTI SR', url: '#' },
        { label: 'Nová publikácia – Ambrozová H. (ed.): Detské infekcie', url: '#' },
        { label: 'Vyhlásenie odborných spoločností SLS k predpisom WHO', url: '#' },
      ],
    },
    {
      icon: 'apartment',
      title: 'Sieť pracovísk na Slovensku',
      text: 'Prehľad infektologických pracovísk a špecializovaných centier podľa zamerania.',
      links: [
        { label: 'Infektologické pracoviská', url: '#' },
        { label: 'Pracoviská pre liečbu vírusových hepatitíd', url: '#' },
        { label: 'HIV/AIDS centrá v SR', url: '#' },
        { label: 'Oddelenia pre cudzokrajné choroby v SR', url: '#' },
      ],
    },
  ];
}
