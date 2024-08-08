import type { Routes } from "@angular/router";
import { NotfoundComponent } from "./pages/notfound/notfound.component";
import { HomeComponent } from "./pages/home/home.component";
import { DrinksComponent } from "./pages/drink/drinks.component";
import { FoodComponent } from "./pages/food/food.component";
import { ShoppingCartComponent } from "./components/shopping-cart/shopping-cart.component";
import { AboutUsComponent } from "./pages/about/about-us/about-us.component";
import { LoginComponent } from "./pages/login/login.component";
import { SignupComponent } from "./pages/signup/signup.component";

export const routes: Routes = [
	{
		path: "",
		redirectTo: "/home",
		pathMatch: "full",
	},
	{
		path: "home",
		component: HomeComponent,
	},
	{
		path: "drink",
		component: DrinksComponent,
	},
	{
		path: "food",
		component: FoodComponent,
	},
	{
		path: "cart",
		component: ShoppingCartComponent,
	},
	{
		path: "about",
		component: AboutUsComponent,
	},
	{
		path: "login",
		component: LoginComponent,
	},
	{
		path: "signup",
		component: SignupComponent,
	},
	{
		path: "**",
		component: NotfoundComponent,
	},
];
