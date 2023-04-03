import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { Guid } from 'guid-typescript';
import { AccountComponent } from 'src/app/account/account.component';
import { ApiResponse, ServerError } from 'src/app/models/api-response.model';
import { CompetenceCreate } from 'src/app/models/competence-create.model';
import { Competence } from 'src/app/models/competence.model';
import { EducationalProgramDetails } from 'src/app/models/educational-program-details.model';
import { ProgramResultCreate } from 'src/app/models/program-result-create.model';
import { ProgramResult } from 'src/app/models/program-result.model';
import { CompetenceService } from 'src/app/services/competence.service';
import { ConfirmationDialogService } from 'src/app/services/confirmation-dialog.service';
import { EducationalProgramService } from 'src/app/services/educational-program.service';
import { ProgramResultService } from 'src/app/services/program-result.service';

@Component({
  selector: 'app-educational-program-details',
  templateUrl: './educational-program-details.component.html',
  styleUrls: ['./educational-program-details.component.scss']
})
export class EducationalProgramDetailsComponent implements OnInit {

  constructor(private activatedRouter: ActivatedRoute, private service: EducationalProgramService, private router: Router,
    public account: AccountComponent, private compService: CompetenceService, private prService: ProgramResultService,
    private formBuilder: FormBuilder, private toastService: HotToastService, private confirmationDialogService: ConfirmationDialogService) { }

  educationalProgram: EducationalProgramDetails
  compFormCreateOpened: boolean = false
  prFormCreateOpened: boolean = false
  createCompForm: FormGroup
  createPRForm: FormGroup
  errorComp: ServerError | null;
  errorPR: ServerError | null;
  page: number = 1;
  tableSize: number = 6;
  onTableDataChange(event: any){
    this.page = event
  }

  ngOnInit(): void {
    this.account.loadPerson()
    this.loadData()
    this.createCompForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    })

    this.createPRForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    })
  }

  loadData(){
    const id = this.activatedRouter.snapshot.paramMap.get('id');
    this.service.getDetails(id!).subscribe({
      next: (res: ApiResponse<EducationalProgramDetails>) => {
        this.educationalProgram = res.result
      }
    })
  }

  compFormOpen(){
    this.compFormCreateOpened = true
  }

  compFormClose(){
    this.compFormCreateOpened = false
  }

  createComp(){
    this.errorComp = null
    this.createCompForm.disable()
    var model: CompetenceCreate = {} as CompetenceCreate
    model.name = this.createCompForm.value.name;
    model.description = this.createCompForm.value.description;
    model.educationalProgramId = this.educationalProgram.id

    this.compService.create(model)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Створення компетентності',
          success: 'Компетентність створена успішно',
          error: 'Помилка створення компетентності',
        }
      )
    )
    .subscribe({
      next: async ()=>{
        await new Promise(f => setTimeout(f, 1000));
        this.createCompForm.enable()
        this.createCompForm.reset()
        this.compFormCreateOpened = false
        this.loadData()
      },
      error: (response: HttpErrorResponse)=>{
        this.createCompForm.enable()
        this.errorComp = response.error?.errors[0]
      }
    })
  }

  deleteComp(comp: Competence){
    this.confirmationDialogService.confirm(`Видалити компетентність ${comp.name}?`)
    .then((confirmed) => {
      if(confirmed){
        this.compService.delete(comp.id)
        .pipe(
          this.toastService.observe(
            {
              loading: 'Видалення',
              success: 'Видалення успішне',
              error: 'Помилка видалення',
            }
          )
        )
        .subscribe({
          next: async ()=>{
            this.loadData()
          }
        })
      }
    }).catch(() => console.log('outside the dialog'))
  }

  prFormOpen(){
    this.prFormCreateOpened = true
  }

  prFormClose(){
    this.prFormCreateOpened = false
  }

  createPR(){
    this.errorPR = null
    this.createPRForm.disable()
    var model: ProgramResultCreate = {} as ProgramResultCreate
    model.name = this.createPRForm.value.name;
    model.description = this.createPRForm.value.description;
    model.educationalProgramId = this.educationalProgram.id

    this.prService.create(model)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Створення програмного результату',
          success: 'Програмний результат створений успішно',
          error: 'Помилка створення програмного результату',
        }
      )
    )
    .subscribe({
      next: async ()=>{
        await new Promise(f => setTimeout(f, 1000));
        this.createPRForm.enable()
        this.createPRForm.reset()
        this.prFormCreateOpened = false
        this.loadData()
      },
      error: (response: HttpErrorResponse)=>{
        this.createPRForm.enable()
        this.errorPR = response.error?.errors[0]
      }
    })
  }

  deletePR(comp: ProgramResult){
    this.confirmationDialogService.confirm(`Видалити програмний результат ${comp.name}?`)
    .then((confirmed) => {
      if(confirmed){
        this.prService.delete(comp.id)
        .pipe(
          this.toastService.observe(
            {
              loading: 'Видалення',
              success: 'Видалення успішне',
              error: 'Помилка видалення',
            }
          )
        )
        .subscribe({
          next: async ()=>{
            this.loadData()
          }
        })
      }
    }).catch(() => console.log('outside the dialog'))
  }
}
