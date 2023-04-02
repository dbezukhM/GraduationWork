import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../models/api-response.model';
import { EducationalProgram } from '../models/educational-program.model';
import { environment } from 'src/environments/environment';
import { EducationalProgramDetails } from '../models/educational-program-details.model';
import { EducationalProgramCreate } from '../models/educational-program-create.model';
import { Guid } from 'guid-typescript';
import { EducationalProgramUpdate } from '../models/educational-program-update.model';

@Injectable({
  providedIn: 'root'
})
export class EducationalProgramService {

  constructor(private httpClient: HttpClient) { }

  getAll(){
    return this.httpClient.get<ApiResponse<EducationalProgram[]>>(environment.baseUrl + 'api/EducationalProgram');
  }

  getDetails(id: string){
    return this.httpClient.get<ApiResponse<EducationalProgramDetails>>(environment.baseUrl + `api/EducationalProgram/${id}`);
  }

  create(model: EducationalProgramCreate){
    return this.httpClient.post<ApiResponse<Guid>>(environment.baseUrl + 'api/EducationalProgram', model);
  }

  update(id: Guid, model: EducationalProgramUpdate){
    return this.httpClient.put(environment.baseUrl + `api/EducationalProgram/${id}`, model);
  }
}
