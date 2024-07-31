import { Component } from '@angular/core';
import {NgForOf} from "@angular/common";
import {
  HlmCardContentDirective,
  HlmCardDescriptionDirective, HlmCardDirective,
  HlmCardFooterDirective, HlmCardHeaderDirective,
  HlmCardTitleDirective
} from "@spartan-ng/ui-card-helm";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-home',
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
    RouterLink,
    HlmCardContentDirective
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
