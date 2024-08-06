import { NgClass, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IProduct } from '@shared/models/product';
import { IProductBranch } from '@shared/models/productBranch';
import { IProductType } from '@shared/models/productType';
import { ShopParams } from '@shared/models/shopParams';
import { ProductHeadingComponent } from '@shop/product-heading/product-heading.component';
import { ProductItemComponent } from '@shop/product-item/product-item.component';
import { ShopService } from '@shop/shop.service';
import { NgxPaginationModule } from 'ngx-pagination'; // <-- import the module

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    ProductItemComponent,
    ProductHeadingComponent,
    NgClass,
    NgxPaginationModule,
    NgFor,
  ],
  templateUrl: './shop.component.html',
})
export class ShopComponent implements OnInit {
  public products: IProduct[] | null = null;
  public productTypes: IProductType[] | null = null;
  public productBrands: IProductBranch[] | null = null;
  public productCount: number = 0;
  public shopParams: ShopParams = {
    brandId: 0,
    typeId: 0,
    sort: 'name',
    pageNumber: 1,
    pageSize: 6,
    search: '',
  };

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getProductTypes();
    this.getProductBrands();
  }

  public onPageChanged(event: number): void {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  public handleSearch(search: string): void {
    this.shopParams.search = search;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public onSelectedSort(sortValue: string): void {
    this.shopParams.sort = sortValue;
    this.getProducts();
  }

  public onBrandSelected(brandId: number): void {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public onTypeSelected(typeId: number): void {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
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
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => {
        if (response !== null) {
          this.products = response.data;
          this.productCount = response.count;
          this.shopParams.pageNumber = response.pageIndex;
          this.shopParams.pageSize = response.pageSize;
        }
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
