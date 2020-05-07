import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarDayHourComponent } from './calendar-day-hour.component';

describe('CalendarDayHourComponent', () => {
  let component: CalendarDayHourComponent;
  let fixture: ComponentFixture<CalendarDayHourComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarDayHourComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarDayHourComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
