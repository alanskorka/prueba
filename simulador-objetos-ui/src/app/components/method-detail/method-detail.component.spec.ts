import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MethodDetailComponent } from './method-detail.component';

describe('MethodDetailComponent', () => {
  let component: MethodDetailComponent;
  let fixture: ComponentFixture<MethodDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MethodDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MethodDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
