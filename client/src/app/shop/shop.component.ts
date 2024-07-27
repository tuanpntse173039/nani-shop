import { CommonModule, NgClass } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IProduct } from '@shared/models/product';
import { IProductBranch } from '@shared/models/productBranch';
import { IProductType } from '@shared/models/productType';
import { ShopService } from '@shop/shop.service';
import { ProductHeadingComponent } from './product-heading/product-heading.component';
import { ProductItemComponent } from './product-item/product-item.component';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent, ProductHeadingComponent, NgClass],
  templateUrl: './shop.component.html',
})
export class ShopComponent implements OnInit {
  public products: IProduct[] = [];
  public productTypes: IProductType[] = [];
  public productBrands: IProductBranch[] = [];
  public productCount: number = 0;
  public pageSize: number = 6;
  public pageNumber: number = 1;
  public selectedBrandId: number = 0;
  public selectedTypeId: number = 0;

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getProductTypes();
    this.getProductBrands();
  }

  public onBrandSelected(brandId: number): void {
    this.selectedBrandId = brandId;
    this.getProducts();
  }

  public onTypeSelected(typeId: number): void {
    this.selectedTypeId = typeId;
    this.getProducts();
  }

  private getProductBrands(): void {
    this.shopService.getProductBrands().subscribe({
      next: (response) => {
        this.productBrands = [{ id: 0, name: 'All' }, ...response];
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  private getProductTypes(): void {
    this.shopService.getProductTypes().subscribe({
      next: (response) => {
        this.productTypes = [{ id: 0, name: 'All' }, ...response];
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  private getProducts(): void {
    this.shopService
      .getProducts(this.selectedBrandId, this.selectedTypeId)
      .subscribe({
        next: (response) => {
          if (response !== null) {
            this.products = response.data;
            this.productCount = response.count;
            this.pageNumber = response.pageIndex;
            this.pageSize = response.pageSize;
          }
        },
        error: (error) => {
          console.log(error);
        },
      });
  }
}
