import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { WorkingProgramDetails } from '../models/working-program-details.model';
import { WorkingProgram } from '../models/working-program.model';

@Injectable({
  providedIn: 'root'
})
export class WorkingProgramService {

  constructor(private httpClient: HttpClient) { }

  downloadFile(subjectId: Guid){
    return this.httpClient.get(environment.baseUrl + `api/WorkingProgram/generateTemplate/${subjectId}`, {
      observe: 'events',
      responseType: 'blob'
    })
  }

  getAll(){
    return this.httpClient.get<ApiResponse<WorkingProgram[]>>(environment.baseUrl + "api/WorkingProgram")
  }

  create(model: FormData){
    return this.httpClient.post<ApiResponse<Guid>>(environment.baseUrl + 'api/WorkingProgram', model)
  }

  getDetails(id: string){
    return this.httpClient.get<ApiResponse<WorkingProgramDetails>>(environment.baseUrl + `api/WorkingProgram/${id}`)
  }

  delete(id: Guid){
    return this.httpClient.delete(environment.baseUrl + `api/WorkingProgram/${id}`)
  }

  getWorkingProgramFile(id: Guid){
    return this.httpClient.get(environment.baseUrl + `api/WorkingProgram/getWorkingProgramFile/${id}`, {
      observe: 'events',
      responseType: 'blob'
    })
  }
  approveWp(id: Guid){
    return this.httpClient.put(environment.baseUrl + `api/WorkingProgram/approve/${id}`, null)
  }
}
