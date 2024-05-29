import { Routes } from '@angular/router';
import {NotfoundComponent} from "./pages/notfound/notfound.component";
import {HomeComponent} from "./components/home/home.component";
import {DrinksComponent} from "./pages/drinks/drinks.component";
import {FoodComponent} from "./pages/food/food.component";
import {ShoppingCartComponent} from "./components/shopping-cart/shopping-cart.component";

export const routes: Routes = [
  {
    path:'', redirectTo:'/home', pathMatch:'full'
  },
  {
    path:'home',
    component:HomeComponent
  },
  {
    path:'drink',
    component:DrinksComponent
  },
  {
    path:'food',
    component:FoodComponent
  },
  {
    path:'cart',
    component:ShoppingCartComponent
  },
  {
    path:'**',
    component:NotfoundComponent

  },
];
