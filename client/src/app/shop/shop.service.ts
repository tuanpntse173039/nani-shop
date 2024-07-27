import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '@shared/models/pagination';
import { IProductBranch } from '@shared/models/productBranch';
import { IProductType } from '@shared/models/productType';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private readonly baseUrl: string = 'http://localhost:5009/api/';

  constructor(private http: HttpClient) {}

  getProducts(
    brandId?: number,
    typeId?: number,
  ): Observable<IPagination | null> {
    let params = new HttpParams();
    if (brandId) {
      params = params.append('brandId', brandId);
    }
    if (typeId) {
      params = params.append('typeId', typeId);
    }

    return this.http
      .get<IPagination>(this.baseUrl + 'products', {
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
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }

  getProductBrands(): Observable<IProductBranch[]> {
    return this.http.get<IProductBranch[]>(this.baseUrl + 'products/brands');
  }
}
