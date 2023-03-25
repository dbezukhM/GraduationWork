import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { AuthenticatedResponse } from '../models/authenticated-response.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private httpClient: HttpClient) { }

  login(email: string, password: string){
    var body = {
      Email: email,
      Password: password
    };
    return this.httpClient.post<ApiResponse<AuthenticatedResponse>>(environment.baseUrl + 'api/Account/login', body);
  }
}
