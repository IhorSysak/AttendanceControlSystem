import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceTrackingComponent } from './attendance-tracking.component';

describe('AttendanceTrackingComponent', () => {
  let component: AttendanceTrackingComponent;
  let fixture: ComponentFixture<AttendanceTrackingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AttendanceTrackingComponent]
    });
    fixture = TestBed.createComponent(AttendanceTrackingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
