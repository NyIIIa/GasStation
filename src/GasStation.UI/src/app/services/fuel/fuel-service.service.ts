import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Fuel} from "../../models/fuel/Fuel";

@Injectable({
  providedIn: 'root'
})
export class FuelService {
  private url = "https://localhost:7187/api/fuel"
  constructor(private httpClient: HttpClient) { }

  Add(fuel: any){
    return this.httpClient.post<any>(`${this.url}/create`, fuel);
  }

  Update(fuel: any){
    return this.httpClient.put<any>(`${this.url}/update`, fuel);
  }

  GetAll(){
    return this.httpClient.get<Fuel[]>(`${this.url}/getAll`);
  }
}
