import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/auth.service';

import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  imports: [MatCardModule, MatIconModule, MatChipsModule],
  selector: 'app-home',
  styleUrl: './home.scss',
  templateUrl: './home.html',
})
export class Home {
  private authService = inject(AuthService);

  firstName = this.authService.getFirstName();
  lastName = this.authService.getLastName();
  role = this.authService.getRole();
}
