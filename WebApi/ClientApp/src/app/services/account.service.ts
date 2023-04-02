import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { AuthenticatedResponse } from '../models/authenticated-response.model';
import { ChangePassword } from '../models/change-password.model';
import { Person } from '../models/person.model';
import { PersonCreate } from '../models/person-create.model';

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

  getPersonById(id: string) {
    return this.httpClient.get<ApiResponse<Person>>(environment.baseUrl + `api/Account/${id}`);
  }

  getPersonEmailId(email: string) {
    return this.httpClient.get<ApiResponse<Person>>(environment.baseUrl + `api/Account/getByEmail/${email}`);
  }

  getAuthorizedPerson() {
    return this.httpClient.get<ApiResponse<Person>>(environment.baseUrl + 'api/Account/getAuthorized');
  }

  changePassword(req: ChangePassword) {
    return this.httpClient.post(environment.baseUrl + 'api/Account/changePassword', req);
  }

  getAll() {
    return this.httpClient.get<ApiResponse<Person[]>>(environment.baseUrl + 'api/Account/persons')
  }

  createPerson(model: PersonCreate){
    return this.httpClient.post<ApiResponse<Guid>>(environment.baseUrl + "api/Account", model)
  }
}
