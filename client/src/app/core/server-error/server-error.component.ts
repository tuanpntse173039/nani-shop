import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  standalone: true,
  imports: [],
  templateUrl: './server-error.component.html',
})
export class ServerErrorComponent {
  public serverError;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.serverError = navigation?.extras?.state?.['error'];
  }
}
