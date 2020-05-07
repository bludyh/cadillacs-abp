import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarWeekWeekComponent } from './calendar-week-week.component';

describe('CalendarWeekWeekComponent', () => {
  let component: CalendarWeekWeekComponent;
  let fixture: ComponentFixture<CalendarWeekWeekComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarWeekWeekComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarWeekWeekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
