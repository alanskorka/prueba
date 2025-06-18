import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InterfaceMethodDetailComponent } from './interface-method-detail.component';

describe('InterfaceMethodDetailComponent', () => {
  let component: InterfaceMethodDetailComponent;
  let fixture: ComponentFixture<InterfaceMethodDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InterfaceMethodDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InterfaceMethodDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
