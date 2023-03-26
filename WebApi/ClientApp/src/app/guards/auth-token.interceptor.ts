import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountComponent } from '../account/account.component';

@Injectable()
export class AuthTokenInterceptor implements HttpInterceptor {

  constructor(private accountComponent: AccountComponent) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if(this.accountComponent.isAuthenticated()){
      request = request.clone({
        setHeaders: {
          Authorization: 'bearer ' + this.accountComponent.getToken()!
        }
      })
    }
    return next.handle(request);
  }
}
