import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarMonthWeekComponent } from './calendar-month-week.component';

describe('CalendarMonthWeekComponent', () => {
  let component: CalendarMonthWeekComponent;
  let fixture: ComponentFixture<CalendarMonthWeekComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarMonthWeekComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarMonthWeekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
