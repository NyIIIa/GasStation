import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewReportInvoicesComponent } from './view-report-invoices.component';

describe('ViewReportInvoicesComponent', () => {
  let component: ViewReportInvoicesComponent;
  let fixture: ComponentFixture<ViewReportInvoicesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewReportInvoicesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewReportInvoicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
