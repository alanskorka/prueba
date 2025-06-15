import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InterfaceMethodFormComponent } from './interface-method-form.component';

describe('InterfaceMethodFormComponent', () => {
  let component: InterfaceMethodFormComponent;
  let fixture: ComponentFixture<InterfaceMethodFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InterfaceMethodFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InterfaceMethodFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
