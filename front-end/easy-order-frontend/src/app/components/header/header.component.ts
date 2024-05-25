import { Component } from '@angular/core';
import {HlmIconComponent, HlmIconModule, provideIcons} from "@spartan-ng/ui-icon-helm";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {NgIconsModule} from "@ng-icons/core";
import {lucideLogIn, lucideShoppingCart} from "@ng-icons/lucide"; //ne ucitava iz spartana ikone, ne znam sto
@Component({
  imports: [HlmIconComponent, HlmIconModule, RouterLink, RouterLinkActive],
  selector: 'app-header',
  standalone: true,
  styleUrl: './header.component.css',
  templateUrl: './header.component.html',
  providers: [provideIcons({lucideLogIn, lucideShoppingCart})] //non spartan ikone
})
export class HeaderComponent {

}
