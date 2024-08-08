import { Injectable, signal } from "@angular/core";
import { ICartItem, ShoppingCart } from "../../models/ICartItem";
import { cartArray } from "./cartArray";
@Injectable({
	providedIn: "root",
})
export class CartService {
	cart = signal<ShoppingCart>({
		items: cartArray,
		totalAmount: this.calculateTotalAmount(cartArray),
	});

	private calculateTotalAmount(items: ICartItem[]): number {
		return items.reduce(
			(total, item) => (total = item.totalPrice * item.quantity),
			0,
		);
	}

	addItem(item: ICartItem) {
		this.cart.update((currentCart) => {
			const existingItem =
				currentCart.items.length &&
				currentCart.items.find((i: ICartItem) => i.item.id === item.item.id);

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
	removeItem(productId: number) {
		this.cart.update((currentCart) => {
			const item = currentCart.items.find(
				(i: ICartItem) => i.item.id === productId,
			);

			if (item) {
				if (item.quantity > 1) {
					// Reduce quantity and adjust total price
					item.quantity -= 1;
					currentCart.totalAmount -= item.totalPrice;
				} else {
					// Remove item if quantity is 1 or less
					currentCart.totalAmount -= item.totalPrice * item.quantity;
					currentCart.items = currentCart.items.filter(
						(i) => i.item.id !== productId,
					);
				}
			}
			return currentCart;
		});
	}
}
