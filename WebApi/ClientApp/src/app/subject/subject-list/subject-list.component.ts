import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HotToastService } from '@ngneat/hot-toast';
import { AccountComponent } from 'src/app/account/account.component';
import { ApiResponse } from 'src/app/models/api-response.model';
import { Subject } from 'src/app/models/subject.model';
import { SubjectService } from 'src/app/services/subject.service';

@Component({
  selector: 'app-subject-list',
  templateUrl: './subject-list.component.html',
  styleUrls: ['./subject-list.component.scss']
})
export class SubjectListComponent implements OnInit {

  constructor(private subjectService: SubjectService, public account: AccountComponent,
    private formBuilder: FormBuilder, private toast: HotToastService) { }

  subjects: Subject[] = []
  searchedSubjects: Subject[] = []
  searchName: string =''
  searchEducationalProgram: string =''
  searchSelectiveBlock: string =''

  page: number = 1;
  tableSize: number = 7;
  onTableDataChange(event: any){
    this.page = event
  }

  ngOnInit(): void {
    this.account.loadPerson()
    this.subjectService.getAll().subscribe({
      next: (result: ApiResponse<Subject[]>) =>{
        this.subjects = result.result
        this.searchedSubjects = result.result
      },
      error: () => {
        this.toast.error("Помилка з'єднання, спробуйте знову")
      }
    })
  }

  search(){
    this.page = 1
    this.searchedSubjects = this.subjects
    if(this.searchName != ''){
      this.searchedSubjects = this.searchedSubjects.filter((val) =>
        val.name.toLowerCase().includes(this.searchName)
      );
    }
    if(this.searchEducationalProgram != ''){
      this.searchedSubjects = this.searchedSubjects.filter((val) =>
        val.educationalProgramName.toLowerCase().includes(this.searchEducationalProgram)
      );
    }
    if(this.searchSelectiveBlock != ''){
      this.searchedSubjects = this.searchedSubjects.filter((val) =>
        val.selectiveBlockName.toLowerCase().includes(this.searchSelectiveBlock)
      );
    }
  }

}
