import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map, of } from 'rxjs';
import { environment } from '../../environments/environment';
import { EventItem } from '../models/event.model';

interface EventsResponse {
  items: EventItem[];
  totalCount: number;
}

@Injectable({ providedIn: 'root' })
export class EventService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/Event`;

  getAll(): Observable<EventItem[]> {
  return this.http.get<EventsResponse>(this.url).pipe(map((res) => res.items));
}

  getUpcoming(): Observable<EventItem[]> {
    const today = new Date().toISOString().slice(0, 10);

    return this.getAll().pipe(
      map((events) =>
        events
          .filter((e) => (e.end ?? e.start) >= today)
          .sort((a, b) => a.start.localeCompare(b.start)),
      ),
      catchError((err) => {
        console.error('Chyba pri načítaní podujatí', err);
        return of([]);
      }),
    );
  }
}