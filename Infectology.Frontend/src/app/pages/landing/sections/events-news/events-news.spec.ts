import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EventsNews } from './events-news';

describe('EventsNews', () => {
  let component: EventsNews;
  let fixture: ComponentFixture<EventsNews>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventsNews],
    }).compileComponents();

    fixture = TestBed.createComponent(EventsNews);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
