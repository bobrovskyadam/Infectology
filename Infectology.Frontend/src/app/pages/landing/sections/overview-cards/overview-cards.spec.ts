import { ComponentFixture, TestBed } from '@angular/core/testing';
import { OverviewCards } from './overview-cards';

describe('OverviewCards', () => {
  let component: OverviewCards;
  let fixture: ComponentFixture<OverviewCards>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OverviewCards],
    }).compileComponents();

    fixture = TestBed.createComponent(OverviewCards);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
