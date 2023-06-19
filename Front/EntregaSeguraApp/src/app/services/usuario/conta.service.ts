import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, map, take } from 'rxjs';
import { User } from 'src/app/models/user';

@Injectable({
  providedIn: 'root'
})
export class ContaService {
  private urlBase: string = 'https://localhost:5001/api/conta';
  private currentUserSource = new ReplaySubject<User | null>(1);

  public currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  public login(model: any): Observable<void> {
    return this.http.post(this.urlBase + '/login', model).pipe(
      take(1),
      map((response: any) => {
        const user: User = {
          email: response.data.email,
          token: response.data.token
        };

        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  public setCurrentUser(user: User): void {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  public logout(): void {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentUserSource.complete();
  }
}
