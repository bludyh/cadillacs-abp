import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FoundCourseComponent } from './found-course.component';

describe('FoundCourseComponent', () => {
  let component: FoundCourseComponent;
  let fixture: ComponentFixture<FoundCourseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FoundCourseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FoundCourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
