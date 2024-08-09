import { Component, OnInit } from '@angular/core';
import {HlmIconComponent, HlmIconModule, provideIcons} from "@spartan-ng/ui-icon-helm";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {NgIconsModule} from "@ng-icons/core";
import {lucideLogIn, lucideShoppingCart} from "@ng-icons/lucide";
import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';
import { AuthService } from '../../services/auth/auth.service';
@Component({
  imports: [HlmIconComponent, HlmIconModule, RouterLink, RouterLinkActive, HlmButtonDirective],
  selector: 'app-header',
  standalone: true,
  styleUrl: './header.component.css',
  templateUrl: './header.component.html',
  providers: [provideIcons({lucideLogIn, lucideShoppingCart})] //non spartan ikone
})
export class HeaderComponent {

  loggedIn:boolean = false;
  
  constructor(private auth:AuthService) {
  }
  

  logout() {
    this.auth.signOut();
  }
}
