import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { Guid } from 'guid-typescript';
import { AccountComponent } from 'src/app/account/account.component';
import { ApiResponse, ServerError } from 'src/app/models/api-response.model';
import { IdNameModel } from 'src/app/models/id-name-model.model';
import { WorkingProgram } from 'src/app/models/working-program.model';
import { LookupService } from 'src/app/services/lookup.service';
import { WorkingProgramService } from 'src/app/services/working-program.service';

@Component({
  selector: 'app-working-program-list',
  templateUrl: './working-program-list.component.html',
  styleUrls: ['./working-program-list.component.scss']
})
export class WorkingProgramListComponent implements OnInit {

  workingPrograms: WorkingProgram[] = []
  searchedworkingPrograms: WorkingProgram[] = []
  searchName: string =''
  searchSubjectName: string =''

  createForm: FormGroup
  createFormOpened: boolean = false;
  error: ServerError | null;
  subjects: IdNameModel[] = []
  file: File
  fileName: string = ''

  page: number = 1;
  tableSize: number = 7;
  onTableDataChange(event: any){
    this.page = event
  }
  
  constructor(private service: WorkingProgramService, public account: AccountComponent, private lookup: LookupService,
    private formBuilder: FormBuilder, private toastService: HotToastService, private router: Router) { }

  ngOnInit(): void {
    this.account.loadPerson()
    this.loadData()

    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      subjectId: ['', Validators.required],
      file: ['', Validators.required],
    })
  }

  loadData(){
    this.service.getAll().subscribe({
      next: (res: ApiResponse<WorkingProgram[]>) => {
        this.workingPrograms = res.result
        if(!this.account.isAuthenticated()){
          this.workingPrograms = this.workingPrograms.filter(x => x.isAvailable)
        }
        this.searchedworkingPrograms = this.workingPrograms
        this.lookup.getSubjects().subscribe({
          next: (res: ApiResponse<IdNameModel[]>) => {
            this.subjects = res.result
          }
        })
      }
    })
  }

  search(){
    this.searchedworkingPrograms = this.workingPrograms
    if(this.searchName != ''){
      this.searchedworkingPrograms = this.searchedworkingPrograms.filter((val) =>
        val.name.toLowerCase().includes(this.searchName)
      );
    }
    if(this.searchSubjectName != ''){
      this.searchedworkingPrograms = this.searchedworkingPrograms.filter((val) =>
        val.subjectName.toLowerCase().includes(this.searchSubjectName)
      );
    }
  }

  openForm(){
    this.createForm.reset()
    this.file = {} as File
    this.fileName = ''
    this.createFormOpened = !this.createFormOpened
  }
  closeForm(){
    this.createFormOpened = !this.createFormOpened
  }
  uploadFile = (files: FileList) => {
    this.file = <File>files[0];
    if (this.file){
      this.fileName = this.file.name
    }
  }

  submit(){
    this.error = null
    this.createForm.disable()
    const formData = new FormData();
    formData.append("name", this.createForm.value.name)
    formData.append("subjectId", this.createForm.value.subjectId)
    formData.append("file", this.file)

    this.service.create(formData)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Обробка',
          success: 'Робоча програма створена успішно',
          error: 'Помилка створення робочої програми',
        }
      )
    )
    .subscribe({
      next: async (res: ApiResponse<Guid>)=>{
        await new Promise(f => setTimeout(f, 1000));
        this.router.navigate([`working-program/${res.result}`])
      },
      error: (response: HttpErrorResponse)=>{
        this.createForm.enable()
        this.error = response.error?.errors[0]
      }
    })
  }
}
