import { Component, OnInit } from '@angular/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { RouterOutlet } from '@angular/router';
import { initFlowbite } from 'flowbite';
import { NavBarComponent } from '@core/nav-bar/nav-bar.component';
import { ShopComponent } from '@shop/shop.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatSlideToggleModule, NavBarComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {
    initFlowbite();
  }
}
