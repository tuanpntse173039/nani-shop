import { environment } from '@/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-test-error',
  standalone: true,
  imports: [],
  templateUrl: './test-error.component.html',
})
export class TestErrorComponent {
  private resourceNotFoundEndpoint: string = 'bugger/tuan';
  private productNotFoundEndpoint: string = 'products/134';
  private serverErrorEndpoint: string = 'bugger/server-error';
  private badRequestEndpoint: string = 'bugger/bad-request';
  private badRequestInfoEndpoint: string = 'products/1mot';
  private baseUrl: string = environment.baseUrl;

  public errors: any;

  constructor(private httpClient: HttpClient) {}
  public getBadRequestInfo(): void {
    this.httpClient.get(this.baseUrl + this.badRequestInfoEndpoint).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => {
        this.errors = err.errors;
      },
    });
  }

  public getBadRequest(): void {
    this.httpClient.get(this.baseUrl + this.badRequestEndpoint).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  public getResourceNotFound(): void {
    this.httpClient
      .get(this.baseUrl + this.resourceNotFoundEndpoint)
      .subscribe({
        next: (res) => {
          console.log(res);
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  public getProductNotFound(): void {
    this.httpClient.get(this.baseUrl + this.productNotFoundEndpoint).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  public getServerError(): void {
    this.httpClient.get(this.baseUrl + this.serverErrorEndpoint).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
