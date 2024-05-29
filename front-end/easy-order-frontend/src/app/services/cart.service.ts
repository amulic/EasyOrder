import {Injectable, signal} from '@angular/core';
import {ICartItem, ShoppingCart} from "../models/ICartItem";
import {mockItems} from "./mock.Items";
@Injectable({
  providedIn: 'root'
})
export class CartService {
  cart = signal<ShoppingCart>({
    items:mockItems,
    totalAmount: this.calculateTotalAmount(mockItems)
  });
  constructor() { }

  private calculateTotalAmount(items:ICartItem[]):number {
    return items.reduce((total,item)=> total = item.totalPrice * item.quantity, 0);
  }

  addItem(item:ICartItem) {
    this.cart.update((currentCart) => {
      const existingItem = currentCart.items.find(
        (i) => i.item.id === item.item.id
      );

      if (existingItem) {
        // Increment quantity if item already exists
        existingItem.quantity += item.quantity;
      } else {
        // Add the new item if it doesn't exist
        currentCart.items.push(item);
      }

      currentCart.totalAmount += item.totalPrice * item.quantity;

      return currentCart;
    });
  }
    removeItem(productId:number) {
      this.cart.update((currentCart)=> {
        const item = currentCart.items.find((i)=> i.item.id===productId);

        if(item) {
          currentCart.totalAmount -= item.totalPrice * item.quantity;
          currentCart.items = currentCart.items.filter(
            (i) => i.item.id !== productId
          );
        }
        return currentCart;
      })
    }

}
