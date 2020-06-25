import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPageTeacherComponent } from './main-page-teacher.component';

describe('MainPageTeacherComponent', () => {
  let component: MainPageTeacherComponent;
  let fixture: ComponentFixture<MainPageTeacherComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainPageTeacherComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainPageTeacherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
