import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MethodFormComponent } from './method-form.component';

describe('MethodFormComponent', () => {
  let component: MethodFormComponent;
  let fixture: ComponentFixture<MethodFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MethodFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MethodFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
