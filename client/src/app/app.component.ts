import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { RouterOutlet } from '@angular/router';
import { initFlowbite } from 'flowbite';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';
import { NavBarComponent } from './nav-bar/nav-bar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatSlideToggleModule, NavBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title: string = 'TuanPNT17';
  products: IProduct[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    initFlowbite();

    this.http
      .get<IPagination>('http://localhost:5009/api/products')
      .subscribe((response: IPagination) => {
        this.products = response.data;
      });
  }
}
