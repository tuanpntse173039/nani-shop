import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  private busyRequestCount: number = 0;

  constructor(private spinner: NgxSpinnerService) {}

  public busy(): void {
    this.busyRequestCount++;
    this.spinner.show(undefined, {
      type: 'ball-scale-multiple',
      bdColor: 'rgba(255,255,255,0.7)',
      color: '#333333',
    });
  }

  idle(): void {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinner.hide();
    }
  }
}
