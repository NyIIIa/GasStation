import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuelsTableComponent } from './fuels-table.component';

describe('FuelsTableComponent', () => {
  let component: FuelsTableComponent;
  let fixture: ComponentFixture<FuelsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuelsTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FuelsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
