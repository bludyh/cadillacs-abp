import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgressObjectComponent } from './progress-object.component';

describe('ProgressObjectComponent', () => {
  let component: ProgressObjectComponent;
  let fixture: ComponentFixture<ProgressObjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgressObjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgressObjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
