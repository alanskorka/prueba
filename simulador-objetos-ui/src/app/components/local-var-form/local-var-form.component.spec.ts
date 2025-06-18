import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocalVarFormComponent } from './local-var-form.component';

describe('LocalVarFormComponent', () => {
  let component: LocalVarFormComponent;
  let fixture: ComponentFixture<LocalVarFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LocalVarFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LocalVarFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
