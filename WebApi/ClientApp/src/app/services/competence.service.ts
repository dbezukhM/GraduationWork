import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { environment } from 'src/environments/environment';
import { CompetenceCreate } from '../models/competence-create.model';

@Injectable({
  providedIn: 'root'
})
export class CompetenceService {

  constructor(private httpClient: HttpClient) { }

  create(model: CompetenceCreate){
    return this.httpClient.post(environment.baseUrl + 'api/Competence', model)
  }

  delete(id: Guid){
    return this.httpClient.delete(environment.baseUrl + `api/Competence/${id}`)
  }
}
