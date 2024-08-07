import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import 'flowbite';
import { initFlowbite } from 'flowbite';

@Component({
  selector: 'app-product-heading',
  standalone: true,
  imports: [FormsModule, MatSelectModule, MatInputModule, MatFormFieldModule],
  templateUrl: './product-heading.component.html',
})
export class ProductHeadingComponent implements OnInit {
  ngOnInit(): void {
    initFlowbite();
  }
  @Input({ required: true }) productCount = 0;
  @Input({ required: true }) pageSize = 0;
  public selectedSort: string | undefined;
  public sortOptions: { code: string; name: string }[] = [
    { code: 'name', name: 'Alphabetical' },
    { code: 'priceAsc', name: 'Price: Low to High' },
    { code: 'priceDesc', name: 'Price: High to Low' },
  ];
  @Output() sortSelected = new EventEmitter<string>();
  @Output() handleSearch = new EventEmitter<string>();
  public searchValue = '';

  public onSelectedSort(sortValue: string): void {
    this.sortSelected.emit(sortValue);
  }

  public onSearch(formValue: { searchValue: string }): void {
    this.handleSearch.emit(formValue.searchValue);
  }
}
