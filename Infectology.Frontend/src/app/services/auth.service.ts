import { Injectable, inject, signal } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap, finalize } from 'rxjs';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { LogoutResponse } from '../models/logout-response.model';
import { RegisterRequest } from '../models/register-request.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  private token = signal<string | null>(
    localStorage.getItem('token')
  );

  private role = signal<string | null>(
    localStorage.getItem('role')
  );

  private firstName = signal<string | null>(
    localStorage.getItem('firstName')
  );

  private lastName = signal<string | null>(
    localStorage.getItem('lastName')
  );

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
        `${this.apiUrl}/Login`,
        credentials
    ).pipe(
      tap((response) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('role', response.role);
        localStorage.setItem('firstName', response.firstName);
        localStorage.setItem('lastName', response.lastName);

        this.token.set(response.token);
        this.role.set(response.role);
        this.firstName.set(response.firstName);
        this.lastName.set(response.lastName);
      })
    );
  }

  register(request: RegisterRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/Register`, request);
  }

  getToken(): string | null {
    return this.token();
  }

  getRole(): string | null {
    return this.role();
  }

  getFirstName(): string | null {
    return this.firstName();
  }

  getLastName(): string | null {
    return this.lastName();
  }

  isLoggedIn(): boolean {
    return !!this.token();
  }

  logout(): Observable<LogoutResponse> {
    const token = this.token();

    if (!token) {
      this.clearAuthData();

      return new Observable<LogoutResponse>((subscriber) => {
        subscriber.complete();
      });
    }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    
    return this.http.post<LogoutResponse>(
      `${this.apiUrl}/Logout`,
      {},
      { headers }
    ).pipe(
      finalize(() => {
        this.clearAuthData();
      })
    )
  }

  private clearAuthData(): void {
    localStorage.removeItem('token')
    localStorage.removeItem('role')
    localStorage.removeItem('firstName');
    localStorage.removeItem('lastName');

    this.token.set(null);
    this.role.set(null);
    this.firstName.set(null);
    this.lastName.set(null);
  }
}