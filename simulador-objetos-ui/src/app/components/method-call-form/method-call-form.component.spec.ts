import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MethodCallFormComponent } from './method-call-form.component';

describe('MethodCallFormComponent', () => {
  let component: MethodCallFormComponent;
  let fixture: ComponentFixture<MethodCallFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MethodCallFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MethodCallFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
