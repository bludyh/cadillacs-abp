import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SideBarTeacherComponent } from './side-bar-teacher.component';

describe('SideBarTeacherComponent', () => {
  let component: SideBarTeacherComponent;
  let fixture: ComponentFixture<SideBarTeacherComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SideBarTeacherComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SideBarTeacherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
