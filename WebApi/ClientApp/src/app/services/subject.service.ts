import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { SubjectDetails } from '../models/subject-details.model';
import { SubjectUpdate } from '../models/subject-update.model';
import { Subject } from '../models/subject.model';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private httpClient: HttpClient) { }

  getAll(){
    return this.httpClient.get<ApiResponse<Subject[]>>(environment.baseUrl + 'api/Subject')
  }

  getDetails(id: string){
    return this.httpClient.get<ApiResponse<SubjectDetails>>(environment.baseUrl + `api/Subject/${id}`)
  }

  update(id: Guid, model: SubjectUpdate){
    return this.httpClient.put(environment.baseUrl + `api/Subject/${id}`, model)
  }
}
