import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocalVarListComponent } from './local-var-list.component';

describe('LocalVarListComponent', () => {
  let component: LocalVarListComponent;
  let fixture: ComponentFixture<LocalVarListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LocalVarListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LocalVarListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
