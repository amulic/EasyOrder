import { Component } from '@angular/core';
import {
  HlmCardContentDirective,
  HlmCardDescriptionDirective,
  HlmCardDirective,
  HlmCardFooterDirective,
  HlmCardHeaderDirective,
  HlmCardTitleDirective,
} from '@spartan-ng/ui-card-helm';
import {FoodService} from "../../services/food/food.service";
import {IFood} from "../../models/IFood";
import {NgForOf} from "@angular/common";
import {HlmButtonDirective} from "@spartan-ng/ui-button-helm";
import { CartService } from '../../services/cart/cart.service';
import { ICartItem } from '../../models/ICartItem';
@Component({
  selector: 'app-food',
  standalone: true,
    imports: [
        HlmCardDirective,
        HlmCardContentDirective,
        HlmCardHeaderDirective,
        HlmCardDescriptionDirective,
        HlmCardTitleDirective,
        HlmCardContentDirective,
        HlmCardFooterDirective,
        NgForOf,
        HlmButtonDirective,
        HlmButtonDirective
    ],
  templateUrl: './food.component.html',
  styleUrl: './food.component.css'
})
export class FoodComponent {

  item!:ICartItem;

  addToCart(food:IFood) {
    this.item={
      item: food, 
      quantity:1,
      totalPrice:food.price
    };

    console.log(this.item);
    this.cartService.addItem(this.item);
  }

  foodsList:IFood[] =[];
  constructor(private foodService:FoodService, private cartService:CartService) {
      this.foodService.getAllFoods().subscribe((response)=>{
        this.foodsList=response;
      })
  }
}
