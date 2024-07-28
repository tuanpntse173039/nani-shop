import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-heading',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './product-heading.component.html',
})
export class ProductHeadingComponent {
  @Input({ required: true }) productCount: number = 0;
  @Input({ required: true }) pageSize: number = 0;
  public sortOptions: { value: string; display: string }[] = [
    { value: 'name', display: 'Alphabetical' },
    { value: 'priceAsc', display: 'Price: Low to High' },
    { value: 'priceDesc', display: 'Price: High to Low' },
  ];
  @Output() sortSelected = new EventEmitter<string>();
  @Output() handleSearch = new EventEmitter<string>();
  public searchValue: string = '';

  public onSelectedSort(sortValue: string): void {
    this.sortSelected.emit(sortValue);
  }

  public onSearch(formValue: {searchValue: string}): void {
    this.handleSearch.emit(formValue.searchValue);
  }
}
