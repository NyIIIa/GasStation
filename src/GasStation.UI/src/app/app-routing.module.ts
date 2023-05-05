import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {FuelsTableComponent} from "./components/fuel/fuels-table/fuels-table.component";
import {AddFuelComponent} from "./components/fuel/add-fuel/add-fuel.component";
import {UpdateFuelComponent} from "./components/fuel/update-fuel/update-fuel.component";
import {InvoicesTableComponent} from "./components/invoice/invoices-table/invoices-table.component";
import {AddInvoiceComponent} from "./components/invoice/add-invoice/add-invoice.component";
import {DeleteInvoiceComponent} from "./components/invoice/delete-invoice/delete-invoice.component";
import {UpdateInvoiceComponent} from "./components/invoice/update-invoice/update-invoice.component";
import {ReportsTableComponent} from "./components/report/reports-table/reports-table.component";
import {AddReportComponent} from "./components/report/add-report/add-report.component";
import {DeleteReportComponent} from "./components/report/delete-report/delete-report.component";
import {UpdateReportComponent} from "./components/report/update-report/update-report.component";
import {LoginComponent} from "./components/login/login.component";
import {RegisterComponent} from "./components/register/register.component";
import {AuthGuard} from "./guards/auth.guard";
import {Role} from "./models/enums/Role";

const adminAccess : Role[] = [Role.Admin];
const generalAccess : Role[] = [Role.Admin, Role.User];

const routes: Routes = [
  //Authentication & Authorization
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},

  //Fuel
  {path: 'fuels', component: FuelsTableComponent, canActivate: [AuthGuard], data: {roles: generalAccess}},
  {path: 'add-fuel', component: AddFuelComponent, canActivate: [AuthGuard], data: {roles: adminAccess}},
  {path: 'update-fuel', component: UpdateFuelComponent, canActivate: [AuthGuard], data: {roles: adminAccess}},

  //Invoice
  {path: 'invoices', component: InvoicesTableComponent, canActivate: [AuthGuard], data: {roles: generalAccess}},
  {path: 'add-invoice', component: AddInvoiceComponent, canActivate: [AuthGuard], data: {roles: generalAccess}},
  {path: 'delete-invoice', component: DeleteInvoiceComponent, canActivate: [AuthGuard], data: {roles: generalAccess}},
  {path: 'update-invoice', component: UpdateInvoiceComponent, canActivate: [AuthGuard], data: {roles: generalAccess}},

  //Report
  {path: 'reports', component: ReportsTableComponent, canActivate: [AuthGuard], data: {roles: generalAccess}},
  {path: 'add-report', component: AddReportComponent, canActivate: [AuthGuard], data: {roles: generalAccess}},
  {path: 'delete-report', component: DeleteReportComponent, canActivate: [AuthGuard], data: {roles: generalAccess}},
  {path: 'update-report', component: UpdateReportComponent, canActivate: [AuthGuard], data: {roles: generalAccess}}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
