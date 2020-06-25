import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationBarTeacherComponent } from './notification-bar-teacher.component';

describe('NotificationBarTeacherComponent', () => {
  let component: NotificationBarTeacherComponent;
  let fixture: ComponentFixture<NotificationBarTeacherComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NotificationBarTeacherComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NotificationBarTeacherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
