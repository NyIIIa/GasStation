import {Invoice} from "../invoice/Invoice";

export class Report{
  id?: number;
  title?: string;
  totalPrice?: number;
  totalQuantity?: string;
  startDate?: string;
  endDate?: string;
  transactionType?: string;
  invoices?: Invoice[];
}
