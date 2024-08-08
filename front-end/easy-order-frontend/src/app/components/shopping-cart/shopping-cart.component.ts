import { Component } from "@angular/core";
import { NgForOf } from "@angular/common";
import { CartService } from "../../services/cart/cart.service";
import { ICartItem } from "../../models/ICartItem";
import {
	HlmCaptionComponent,
	HlmTableComponent,
	HlmTdComponent,
	HlmThComponent,
	HlmTrowComponent,
} from "@spartan-ng/ui-table-helm";

@Component({
	selector: "app-shopping-cart",
	standalone: true,
	imports: [
		NgForOf,
		HlmTableComponent,
		HlmTrowComponent,
		HlmThComponent,
		HlmTdComponent,
		HlmCaptionComponent,
	],
	templateUrl: "./shopping-cart.component.html",
	styleUrl: "./shopping-cart.component.css",
})
export class ShoppingCartComponent {
	cartItems = this.cartService.cart().items;
	totalAmount = this.cartService.cart().totalAmount;

	constructor(private cartService: CartService) {}

	//addItem():void {
	//	this.cartService.addItem()
	//	this.updateCart();
	//}

	removeItem(productId: number) {
		this.cartService.removeItem(productId);
		this.updateCart();
	}

	updateCart() {
		this.cartItems = this.cartService.cart().items;
		this.totalAmount = this.cartService.cart().totalAmount;
	}
}
