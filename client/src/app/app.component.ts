import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from '@core/nav-bar/nav-bar.component';
import { SectionHeaderComponent } from '@core/section-header/section-header.component';
import { initFlowbite } from 'flowbite';
import { NgxSpinnerModule } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavBarComponent,
    SectionHeaderComponent,
    NgxSpinnerModule,
  ],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {
    initFlowbite();
  }
}
