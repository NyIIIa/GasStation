import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Invoice} from "../../models/invoice/Invoice";
import {Role} from "../../models/enums/Role";

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private url = "https://localhost:7187/api/invoice"

  constructor(private httpClient: HttpClient) { }

  Add(invoice: any){
   return  this.httpClient.post(`${this.url}/create`, invoice);
  }
  Update(invoice: any){
   return  this.httpClient.put(`${this.url}/update`, invoice);
  }
  Delete(invoice: any){
   return  this.httpClient.delete(`${this.url}/delete`, {
     body: invoice,
     headers: {
       'Content-Type': 'application/json'
     }
   });
  }
  GetAll(){
    return this.httpClient.get<Invoice[]>(`${this.url}/getAll`);
  }
}
