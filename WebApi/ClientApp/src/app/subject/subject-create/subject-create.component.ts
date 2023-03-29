import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { Guid } from 'guid-typescript';
import { ApiResponse, ServerError } from 'src/app/models/api-response.model';
import { IdNameModel } from 'src/app/models/id-name-model.model';
import { SubjectCreate } from 'src/app/models/subject-create.model';
import { LookupService } from 'src/app/services/lookup.service';
import { SubjectService } from 'src/app/services/subject.service';

@Component({
  selector: 'app-subject-create',
  templateUrl: './subject-create.component.html',
  styleUrls: ['./subject-create.component.scss']
})
export class SubjectCreateComponent implements OnInit {

  constructor(private service: SubjectService, private lookUp: LookupService, private formBuilder: FormBuilder,
    private router: Router, private toastService: HotToastService) { }

  createForm: FormGroup;
  createModel: SubjectCreate = {} as SubjectCreate
  selectiveBlocks: IdNameModel[]
  finalControlTypes: IdNameModel[]
  educationalPrograms: IdNameModel[]
  error: ServerError | null;

  ngOnInit(): void {
    this.loadFields()
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      credits: ['', Validators.required],
      semester: ['', Validators.required],
      lecturesHours: ['', Validators.required],
      seminarsHours: ['', Validators.required],
      practicalClassesHours: ['', Validators.required],
      laboratoryClassesHours: ['', Validators.required],
      trainingsHours: ['', Validators.required],
      consultationsHours: ['', Validators.required],
      selfWorkHours: ['', Validators.required],
      selectiveBlockId: ['', Validators.required],
      finalControlTypeId: ['', Validators.required],
      educationalProgramId: ['', Validators.required],
    })
  }

  back(){
    this.router.navigate([`/subjects`])
  }

  submit(){
    this.error = null
    this.createForm.disable()
    this.createModel.name = this.createForm.value.name
    this.createModel.credits = this.createForm.value.credits
    this.createModel.semester = this.createForm.value.semester
    this.createModel.lecturesHours = this.createForm.value.lecturesHours
    this.createModel.seminarsHours = this.createForm.value.seminarsHours
    this.createModel.practicalClassesHours = this.createForm.value.practicalClassesHours
    this.createModel.laboratoryClassesHours = this.createForm.value.laboratoryClassesHours
    this.createModel.trainingsHours = this.createForm.value.trainingsHours
    this.createModel.consultationsHours = this.createForm.value.consultationsHours
    this.createModel.selfWorkHours = this.createForm.value.selfWorkHours
    this.createModel.selectiveBlockId = this.createForm.value.selectiveBlockId
    this.createModel.finalControlTypeId = this.createForm.value.finalControlTypeId
    this.createModel.educationalProgramId = this.createForm.value.educationalProgramId

    this.service.create(this.createModel)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Створення дисципліни',
          success: 'Дисципліна створена успішно',
          error: 'Помилка створення',
        }
      )
    )
    .subscribe({
      next: async (res: ApiResponse<Guid>) => {
        await new Promise(f => setTimeout(f, 1000));
        this.router.navigate([`subject/${res.result}`])
      },
      error: (response: HttpErrorResponse) => {
        this.createForm.enable()
        this.error = response.error?.errors[0]
      }
    })
  }

  loadFields(){
    this.lookUp.getSelectiveBlocks().subscribe({
      next: (result: ApiResponse<IdNameModel[]>) => {
        this.selectiveBlocks = result.result
      }
    })

    this.lookUp.getFinalControlTypes().subscribe({
      next: (result: ApiResponse<IdNameModel[]>) => {
        this.finalControlTypes = result.result
      }
    })

    this.lookUp.getEducationalPrograms().subscribe({
      next: (result: ApiResponse<IdNameModel[]>) => {
        this.educationalPrograms = result.result
      }
    })
  }

}
