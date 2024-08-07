import { environment } from '@/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { IProduct } from '../shared/models/product';
import { Basket, IBasket, IBasketItem, IBasketTotals } from './../shared/models/basket';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  private readonly baseUrl = environment.baseUrl;
  private readonly basketEndpoint = this.baseUrl + 'basket';
  private basketSource = new BehaviorSubject<IBasket | null>(null);
  private basketTotalSource = new BehaviorSubject<IBasketTotals | null>(null);
  public basket$ = this.basketSource.asObservable();
  public basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) {}

  public incrementBasketItem(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItem = basket?.items.find((value) => value.id === item.id);
    if (basket && foundItem) {
      foundItem.quantity += 1;
      this.setBasket(basket);
    }
  }

  public decrementBasketItem(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItem = basket?.items.find((value) => value.id === item.id);
    if (!basket || !foundItem) return;
    if (foundItem.quantity > 1) {
      foundItem.quantity -= 1;
      this.setBasket(basket);
      return;
    }
    this.removeItemFromBasket(foundItem);
  }

  public removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItem = basket?.items.find((value) => value.id === item.id);
    if (!basket || !foundItem) return;
    if (basket.items.length > 1) {
      basket.items = basket.items.filter((value) => value.id !== foundItem.id);
      this.setBasket(basket);
      return;
    }
    this.removeBasket();
  }
  private removeBasket() {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    this.http.delete<IBasket>(this.basketEndpoint + '?id=' + basket.id).subscribe({
      next: () => {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket_id');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  private calculateTotal() {
    const basket = this.getCurrentBasketValue();
    if (basket === null) return;

    const shipping = 0;
    const subtotal = basket?.items.reduce((sum, item) => sum + item.price * item.quantity, 0);
    const total = shipping + subtotal;
    this.basketTotalSource.next({ shipping, subtotal, total });
  }

  public getBasket(id: string) {
    this.http
      .get<IBasket>(`${this.basketEndpoint}?id=${id}`)
      .pipe(
        map((basket: IBasket) => {
          this.basketSource.next(basket);
          this.calculateTotal();
        })
      )
      .subscribe();
  }

  public setBasket(basket: IBasket) {
    this.http.post<IBasket>(this.basketEndpoint, basket).subscribe({
      next: (res) => {
        this.basketSource.next(res);
        this.calculateTotal();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  public getCurrentBasketValue(): IBasket | null {
    return this.basketSource.value;
  }

  public addItemToBasket(item: IProduct, quantity = 1) {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item, quantity);
    const basket: IBasket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const existedItem = items.find((item) => item.id === itemToAdd.id);
    if (!existedItem) {
      return [...items, itemToAdd];
    }
    existedItem.quantity += quantity;
    return [...items];
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity,
      pictureUrl: item.pictureUrl,
      brand: item.productBrand,
      type: item.productType,
    };
  }
}
