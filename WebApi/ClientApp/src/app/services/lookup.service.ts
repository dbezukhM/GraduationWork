import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { Competence } from '../models/competence.model';
import { IdNameModel } from '../models/id-name-model.model';
import { ProgramResult } from '../models/program-result.model';

@Injectable({
  providedIn: 'root'
})
export class LookupService {

  constructor(private httpClient: HttpClient) { }

  getSelectiveBlocks(){
    return this.httpClient.get<ApiResponse<IdNameModel[]>>(environment.baseUrl + 'api/Lookup/selectiveBlocks')
  }

  getFinalControlTypes(){
    return this.httpClient.get<ApiResponse<IdNameModel[]>>(environment.baseUrl + 'api/Lookup/finalControlTypes')
  }

  getEducationalPrograms(){
    return this.httpClient.get<ApiResponse<IdNameModel[]>>(environment.baseUrl + 'api/Lookup/educationalPrograms')
  }

  getCompetences(id: Guid){
    return this.httpClient.get<ApiResponse<Competence[]>>(environment.baseUrl + `api/Lookup/competences/${id}`)
  }

  getProgramResults(id: Guid){
    return this.httpClient.get<ApiResponse<ProgramResult[]>>(environment.baseUrl + `api/Lookup/programResults/${id}`)
  }

  getSubjects(){
    return this.httpClient.get<ApiResponse<IdNameModel[]>>(environment.baseUrl + 'api/Lookup/subjects')
  }
}
