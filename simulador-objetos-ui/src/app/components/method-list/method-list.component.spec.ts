import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MethodListComponent } from './method-list.component';

describe('MethodListComponent', () => {
  let component: MethodListComponent;
  let fixture: ComponentFixture<MethodListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MethodListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MethodListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
