import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProgramResultCreate } from '../models/program-result-create.model';
import { Guid } from 'guid-typescript';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProgramResultService {

  constructor(private httpClient: HttpClient) { }

  create(model: ProgramResultCreate){
    return this.httpClient.post(environment.baseUrl + 'api/ProgramResult', model)
  }

  delete(id: Guid){
    return this.httpClient.delete(environment.baseUrl + `api/ProgramResult/${id}`)
  }
}
