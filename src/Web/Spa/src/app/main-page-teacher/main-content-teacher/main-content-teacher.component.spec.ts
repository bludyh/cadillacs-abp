import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainContentTeacherComponent } from './main-content-teacher.component';

describe('MainContentTeacherComponent', () => {
  let component: MainContentTeacherComponent;
  let fixture: ComponentFixture<MainContentTeacherComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainContentTeacherComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainContentTeacherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
