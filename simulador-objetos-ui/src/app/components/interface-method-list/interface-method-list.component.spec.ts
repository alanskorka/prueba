import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InterfaceMethodListComponent } from './interface-method-list.component';

describe('InterfaceMethodListComponent', () => {
  let component: InterfaceMethodListComponent;
  let fixture: ComponentFixture<InterfaceMethodListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InterfaceMethodListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InterfaceMethodListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
