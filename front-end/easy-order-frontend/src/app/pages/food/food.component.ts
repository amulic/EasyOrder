import { Component } from '@angular/core';
import {
  HlmCardContentDirective,
  HlmCardDescriptionDirective,
  HlmCardDirective,
  HlmCardFooterDirective,
  HlmCardHeaderDirective,
  HlmCardTitleDirective,
} from '@spartan-ng/ui-card-helm';
import {FoodService} from "../../services/food.service";
import {IFood} from "../../models/IFood";
import {NgForOf} from "@angular/common";
import {HlmButtonDirective} from "@spartan-ng/ui-button-helm";
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

  foodsList:IFood[] =[];
  constructor(private foodService:FoodService) {
      this.foodService.getAllFoods().subscribe((response)=>{
        this.foodsList=response;

      })
  }
}
