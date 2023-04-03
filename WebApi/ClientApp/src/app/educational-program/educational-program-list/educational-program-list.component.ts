import { Component, OnInit } from '@angular/core';
import { AccountComponent } from 'src/app/account/account.component';
import { ApiResponse } from 'src/app/models/api-response.model';
import { EducationalProgram } from 'src/app/models/educational-program.model';
import { EducationalProgramService } from 'src/app/services/educational-program.service';

@Component({
  selector: 'app-educational-program-list',
  templateUrl: './educational-program-list.component.html',
  styleUrls: ['./educational-program-list.component.scss']
})
export class EducationalProgramListComponent implements OnInit {

  constructor(private service: EducationalProgramService, public account: AccountComponent) { }
  educationalPrograms: EducationalProgram[] = []
  searchededucationalPrograms: EducationalProgram[] = []
  searchName: string =''
  searchUniversity: string =''
  searchSpecialization: string =''
  searchEducationalProgramsType: string =''

  page: number = 1;
  tableSize: number = 7;
  onTableDataChange(event: any){
    this.page = event
  }
  
  ngOnInit(): void {
    this.account.loadPerson()
    this.loadData()
  }

  loadData(){
    this.service.getAll().subscribe({
      next: (res: ApiResponse<EducationalProgram[]>) => {
        this.educationalPrograms = res.result
        this.searchededucationalPrograms = this.educationalPrograms
      }
    })
  }
  search(){
    this.page = 1
    this.searchededucationalPrograms = this.educationalPrograms
    if(this.searchName != ''){
      this.searchededucationalPrograms = this.searchededucationalPrograms.filter((val) =>
        val.name.toLowerCase().includes(this.searchName)
      );
    }
    if(this.searchUniversity != ''){
      this.searchededucationalPrograms = this.searchededucationalPrograms.filter((val) =>
        val.universityName.toLowerCase().includes(this.searchUniversity)
      );
    }
    if(this.searchSpecialization != ''){
      this.searchededucationalPrograms = this.searchededucationalPrograms.filter((val) =>
        val.specializationName.toLowerCase().includes(this.searchSpecialization)
      );
    }if(this.searchEducationalProgramsType != ''){
      this.searchededucationalPrograms = this.searchededucationalPrograms.filter((val) =>
        val.educationalProgramsTypeName.toLowerCase().includes(this.searchEducationalProgramsType)
      );
    }
  }
}
