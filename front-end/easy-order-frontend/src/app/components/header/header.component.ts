import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { HlmIconComponent, HlmIconModule, provideIcons } from "@spartan-ng/ui-icon-helm";
import { RouterLink, RouterLinkActive } from "@angular/router";
import { lucideLogIn, lucideShoppingCart } from "@ng-icons/lucide";
import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';
import { AuthService } from '../../services/auth/auth.service';
import { IUser } from '../../models/IUser';
import { NgIf } from '@angular/common';
import { Subscription } from 'rxjs';

@Component({
  imports: [HlmIconComponent, HlmIconModule, RouterLink, RouterLinkActive, HlmButtonDirective, NgIf],
  selector: 'app-header',
  standalone: true,
  styleUrls: ['./header.component.css'],
  templateUrl: './header.component.html',
  providers: [provideIcons({ lucideLogIn, lucideShoppingCart })]
})
export class HeaderComponent implements OnInit, OnDestroy {

  user!: IUser | null;
  private subscription!: Subscription;


  constructor(private auth: AuthService) { }

  ngOnInit(): void {
    // Subscribe to the user$ observable to get the current user
    this.subscription = this.auth.user$.subscribe((user) => {
      this.user = user;
    });
  }

  ngOnDestroy(): void {
    // Unsubscribe from the observable to prevent memory leaks
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  logout(): void {
    this.auth.signOut().subscribe({
      next: (response) => {
        alert(response.message);  // Display logout message
      },
      error: (err) => {
        console.error('Error during logout', err);  // Handle any errors
      }
    });
  }
}
