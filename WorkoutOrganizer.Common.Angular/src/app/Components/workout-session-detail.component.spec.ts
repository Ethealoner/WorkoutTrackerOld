import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkoutSessionDetailComponent } from './workout-session-detail.component';

describe('WorkoutSessionDetailComponent', () => {
  let component: WorkoutSessionDetailComponent;
  let fixture: ComponentFixture<WorkoutSessionDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkoutSessionDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkoutSessionDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
