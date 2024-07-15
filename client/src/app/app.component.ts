import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { RouterOutlet } from '@angular/router';
import { initFlowbite } from 'flowbite';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';
import { NavBarComponent } from './core/nav-bar/nav-bar.component';
import { ShopComponent } from "./shop/shop.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatSlideToggleModule, NavBarComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    initFlowbite();
  }
}
