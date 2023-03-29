import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { environment } from 'src/environments/environment';

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
}
