import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MethodCallDetailComponent } from './method-call-detail.component';

describe('MethodCallDetailComponent', () => {
  let component: MethodCallDetailComponent;
  let fixture: ComponentFixture<MethodCallDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MethodCallDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MethodCallDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
