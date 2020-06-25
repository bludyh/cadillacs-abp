import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationBarTeacherComponent } from './navigation-bar-teacher.component';

describe('NavigationBarTeacherComponent', () => {
  let component: NavigationBarTeacherComponent;
  let fixture: ComponentFixture<NavigationBarTeacherComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NavigationBarTeacherComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavigationBarTeacherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
