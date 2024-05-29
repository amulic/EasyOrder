import {IFood} from "./IFood";

export interface ICartItem{
  item: IFood; //ovde se dodaje | IDrink
  quantity:number;
  totalPrice:number;
}

export interface ShoppingCart {
  items:ICartItem[];
  totalAmount:number;
}
