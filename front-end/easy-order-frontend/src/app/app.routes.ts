import { Routes } from '@angular/router';
import {NotfoundComponent} from "./pages/notfound/notfound.component";
import {HomeComponent} from "./components/home/home.component";
import {DrinksComponent} from "./pages/drinks/drinks.component";
import {FoodComponent} from "./pages/food/food.component";

export const routes: Routes = [
  {
    path:'', redirectTo:'/home', pathMatch:'full'
  },
  {
    path:'home',
    component:HomeComponent
  },
  {
    path:'drinks',
    component:DrinksComponent
  },
  {
    path:'food',
    component:FoodComponent
  },
  {
    path:'**',
    component:NotfoundComponent

  },
];
