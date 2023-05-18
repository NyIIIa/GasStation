import {Fuel} from "../fuel/Fuel";

export class Invoice{
  id?: number;
  title?: string;
  createdDate?: string;
  transactionType?: string;
  consumer?: string;
  provider?: string;
  totalPrice?: number;
  totalFuelQuantity?: number;
  fuel?: Fuel;
}
