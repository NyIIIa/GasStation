import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AddReportComponent } from './components/report/add-report/add-report.component';
import { ReportsTableComponent } from './components/report/reports-table/reports-table.component';
import { DeleteReportComponent } from './components/report/delete-report/delete-report.component';
import { UpdateReportComponent } from './components/report/update-report/update-report.component';
import { InvoicesTableComponent } from './components/invoice/invoices-table/invoices-table.component';
import { AddInvoiceComponent } from './components/invoice/add-invoice/add-invoice.component';
import { DeleteInvoiceComponent } from './components/invoice/delete-invoice/delete-invoice.component';
import { UpdateInvoiceComponent } from './components/invoice/update-invoice/update-invoice.component';
import { FuelsTableComponent } from './components/fuel/fuels-table/fuels-table.component';
import { AddFuelComponent } from './components/fuel/add-fuel/add-fuel.component';
import { UpdateFuelComponent } from './components/fuel/update-fuel/update-fuel.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {ErrorCatchingInterceptor} from "./interceptors/error-catching.interceptor";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ToastrModule} from "ngx-toastr";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {TokenInterceptor} from "./interceptors/token.interceptor";
import {MatInputModule} from "@angular/material/input";
import {MatDialogModule} from "@angular/material/dialog";
import {MatButtonModule} from "@angular/material/button";
import { ViewReportInvoicesComponent } from './components/report/view-report-invoices/view-report-invoices.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatNativeDateModule} from "@angular/material/core";



export const interceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorCatchingInterceptor, multi: true},
  { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    AddReportComponent,
    ReportsTableComponent,
    DeleteReportComponent,
    UpdateReportComponent,
    InvoicesTableComponent,
    AddInvoiceComponent,
    DeleteInvoiceComponent,
    UpdateInvoiceComponent,
    FuelsTableComponent,
    AddFuelComponent,
    UpdateFuelComponent,
    ViewReportInvoicesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    HttpClientModule,
    FontAwesomeModule,
    MatInputModule,
    FormsModule,
    MatDialogModule,
    MatButtonModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule
  ],
  providers: [interceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
