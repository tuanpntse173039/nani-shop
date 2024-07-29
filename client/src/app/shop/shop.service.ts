import { environment } from '@/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '@shared/models/pagination';
import { IProduct } from '@shared/models/product';
import { IProductBranch } from '@shared/models/productBranch';
import { IProductType } from '@shared/models/productType';
import { ShopParams } from '@shared/models/shopParams';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private readonly baseUrl: string = environment.baseUrl;
  private readonly productEndpoint = 'products/';
  private readonly productTypeEndpoint = 'products/types';
  private readonly productBrandEndpoint = 'products/brands';

  constructor(private http: HttpClient) {}

  getProduct(id: number): Observable<IProduct | null> {
    return this.http.get<IProduct>(this.baseUrl + this.productEndpoint + id);
  }

  getProducts(shopParams: ShopParams): Observable<IPagination | null> {
    let params = new HttpParams();
    const { brandId, typeId, sort, search, pageNumber, pageSize } = shopParams;
    if (brandId) {
      params = params.append('brandId', brandId);
    }
    if (typeId) {
      params = params.append('typeId', typeId);
    }
    if (sort) {
      params = params.append('sort', sort);
    }
    if (search) {
      params = params.append('search', search);
    }
    if (pageNumber) {
      params = params.append('pageIndex', pageNumber);
    }
    if (pageSize) {
      params = params.append('pageSize', pageSize);
    }

    return this.http
      .get<IPagination>(this.baseUrl + this.productEndpoint, {
        observe: 'response',
        params: params,
      })
      .pipe(
        map((response) => {
          return response.body;
        }),
      );
  }

  getProductTypes(): Observable<IProductType[]> {
    return this.http.get<IProductType[]>(
      this.baseUrl + this.productTypeEndpoint,
    );
  }

  getProductBrands(): Observable<IProductBranch[]> {
    return this.http.get<IProductBranch[]>(
      this.baseUrl + this.productBrandEndpoint,
    );
  }
}
