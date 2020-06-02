import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarWeekDayComponent } from './calendar-week-day.component';

describe('CalendarWeekDayComponent', () => {
  let component: CalendarWeekDayComponent;
  let fixture: ComponentFixture<CalendarWeekDayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarWeekDayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarWeekDayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

