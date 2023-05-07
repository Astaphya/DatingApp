import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // when we get the user we want to take it only once. Subscriptions will be cancelled.
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        // if user is not null, we add the token to the request
        if (user)
          request = request.clone({
            setHeaders: { Authorization: `Bearer ${user.token}` }

          })
      }

    })
    return next.handle(request);
  }
}


