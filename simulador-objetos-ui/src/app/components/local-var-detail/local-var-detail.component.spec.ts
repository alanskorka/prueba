import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocalVarDetailComponent } from './local-var-detail.component';

describe('LocalVarDetailComponent', () => {
  let component: LocalVarDetailComponent;
  let fixture: ComponentFixture<LocalVarDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LocalVarDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LocalVarDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
