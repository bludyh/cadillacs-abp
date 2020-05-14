import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarMonthMonthComponent } from './calendar-month-month.component';

describe('CalendarMonthMonthComponent', () => {
  let component: CalendarMonthMonthComponent;
  let fixture: ComponentFixture<CalendarMonthMonthComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarMonthMonthComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarMonthMonthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
