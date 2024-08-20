import { environment } from '@/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { IUser } from '@shared/models/user';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private readonly basedUrl: string = environment.baseUrl;
  private readonly accountEndpoint: string = this.basedUrl + 'account';
  private readonly loginEndpoint: string = this.basedUrl + 'account/login';
  private readonly registerEndpoint: string = this.basedUrl + 'account/register';
  private readonly addressEndpoint: string = this.basedUrl + 'account/address';
  private readonly emailExistsEndpoint: string = this.basedUrl + 'account/emailexists';

  private currentUserSource = new BehaviorSubject<IUser | null>(null);
  public currentUser$ = this.currentUserSource.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  public loadCurrentUser(token: string) {
    //1. Add Authorization Bearer to the header
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    //2. Get the current user
    return this.http.get<IUser>(this.accountEndpoint, { headers }).pipe(
      map((user: IUser) => {
        if (user) {
          this.currentUserSource.next(user);
        }
      })
    );
  }

  public login(value: { email: string; password: string }) {
    return this.http.post<IUser>(this.loginEndpoint, value).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  public register(value: { displayName: string; email: string; password: string }) {
    return this.http.post<IUser>(this.registerEndpoint, value).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
        }
      })
    );
  }

  public logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  public checkEmailExist(email: string) {
    return this.http.get<boolean>(this.emailExistsEndpoint + '?email=' + email);
  }
}
