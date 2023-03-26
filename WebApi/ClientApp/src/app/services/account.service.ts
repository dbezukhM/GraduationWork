import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { AuthenticatedResponse } from '../models/authenticated-response.model';
import { Person } from '../models/person.model';

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

  logOut() {
    return this.httpClient.post(environment.baseUrl + 'api/Account/LogOut', null);
  }

  getPersonById(id: Guid) {
    return this.httpClient.get<ApiResponse<Person>>(environment.baseUrl + 'api/Account/');
  }

  getPersonEmailId(email: string) {
    return this.httpClient.get<ApiResponse<Person>>(environment.baseUrl + 'api/Account/getByEmail');
  }

  getAuthorizedPerson() {
    return this.httpClient.get<ApiResponse<Person>>(environment.baseUrl + 'api/Account/getAuthorized');
  }
}
