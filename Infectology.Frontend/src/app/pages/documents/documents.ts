import { Component, inject, signal } from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../services/auth.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

interface DocumentItem {
  id: number;
  name: string;
  description: string | null;
  category: string;
  fileType: 'pdf' | 'doc' | 'docx';
}

@Component({
  imports: [
    MatListModule, 
    MatIconModule, 
    MatDividerModule, 
    MatCardModule, 
    MatButtonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule
  ],
  selector: 'app-documents',
  styleUrl: './documents.scss',
  templateUrl: './documents.html',
})
export class Documents {

  readonly isAdmin = inject(AuthService).getRole() === 'Admin';
  readonly showAddForm = signal(false);
  readonly selectedFile = signal<File | null>(null); 

  readonly addForm = new FormGroup({
    name: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required],
    }),
    description: new FormControl('', { nonNullable: true }),
    category: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required],
    }),
  });

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.selectedFile.set(input.files?.[0] ?? null);
  }

  closeAddForm(): void {
    this.addForm.reset();
    this.selectedFile.set(null);
    this.showAddForm.set(false);
  }

  readonly documents = signal<DocumentItem[]>([]);
}
