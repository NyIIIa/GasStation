import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Invoice} from "../../models/invoice/Invoice";
import {Report} from "../../models/report/Report";

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private url = "https://localhost:7187/api/report"

  constructor(private httpClient: HttpClient) { }

  Add(report: any){
    return this.httpClient.post(`${this.url}/create`, report);
  }
  Update(report: any){
    return  this.httpClient.put(`${this.url}/update`, report);
  }
  Delete(report: any){
    return  this.httpClient.delete(`${this.url}/delete`, {
      body: report,
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }
  GetAll(){
    return this.httpClient.get<Report[]>(`${this.url}/getAll`);
  }
}
