import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { Guid } from 'guid-typescript';
import { ApiResponse, ServerError } from 'src/app/models/api-response.model';
import { EducationalProgramCreate } from 'src/app/models/educational-program-create.model';
import { IdNameModel } from 'src/app/models/id-name-model.model';
import { ConfirmationDialogService } from 'src/app/services/confirmation-dialog.service';
import { EducationalProgramService } from 'src/app/services/educational-program.service';
import { LookupService } from 'src/app/services/lookup.service';

@Component({
  selector: 'app-educational-program-create',
  templateUrl: './educational-program-create.component.html',
  styleUrls: ['./educational-program-create.component.scss']
})
export class EducationalProgramCreateComponent implements OnInit {

  constructor(private service: EducationalProgramService, private lookUp: LookupService, private formBuilder: FormBuilder,
    private router: Router, private toastService: HotToastService, private confirmationDialogService: ConfirmationDialogService) { }

  createForm: FormGroup;
  createModel: EducationalProgramCreate = {} as EducationalProgramCreate;
  faculties: IdNameModel[] = []
  specializations: IdNameModel[] = []
  educationalProgramsTypes: IdNameModel[] = []
  areaOfExpertiselistlist: IdNameModel[] = []
  universities: IdNameModel[] = []
  error: ServerError | null;

  ngOnInit(): void {
    this.loadData()
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      areaOfExpertise: ['', Validators.required],
      specialization: ['', Validators.required],
      university: ['', Validators.required],
      faculty: ['', Validators.required],
      educationalProgramsType: ['', Validators.required],
    })
  }

  loadData(){
    this.lookUp.getAreaOfExpertise().subscribe({
      next: (res: ApiResponse<IdNameModel[]>) => {
        this.areaOfExpertiselistlist = res.result.map(x => {
          x.name = this.capitalize(x.name)
          return x;
        })
      }
    })
    this.lookUp.getUniversities().subscribe({
      next: (res: ApiResponse<IdNameModel[]>) => {
        this.universities = res.result.map(x => {
          x.name = this.capitalize(x.name)
          return x;
        })
      }
    })
    this.lookUp.getEducationalProgramsTypes().subscribe({
      next: (res: ApiResponse<IdNameModel[]>) => {
        this.educationalProgramsTypes = res.result.map(x => {
          x.name = this.capitalize(x.name)
          return x;
        })
      }
    })
  }

  back(){
    this.confirmationDialogService.confirm('Скасувати зміни?')
    .then((confirmed) => {
      if(confirmed){
        this.router.navigate(["/educational-programs"])
      }
    }).catch(() => console.log('outside the dialog'))
  }

  onUniversityChange(event: any){
    var universityId = this.createForm.value.university;
    this.lookUp.getFaculties(universityId).subscribe({
      next: (res: ApiResponse<IdNameModel[]>) => {
        this.faculties = res.result.map(x => {
          x.name = this.capitalize(x.name)
          return x;
        })
        this.createForm.get('faculty')?.setValue(this.faculties[0].id)
      }
    })
  }

  onAreaOfExpertiseChange(event: any){
    var areaOfExpertiseId = this.createForm.value.areaOfExpertise;
    this.lookUp.getSpecializations(areaOfExpertiseId).subscribe({
      next: (res: ApiResponse<IdNameModel[]>) => {
        this.specializations = res.result.map(x => {
          x.name = this.capitalize(x.name)
          return x;
        })
        this.createForm.get('specialization')?.setValue(this.specializations[0].id)
      }
    })
  }

  submit(){
    this.error = null
    this.createForm.disable()
    this.createModel.name = this.createForm.value.name
    this.createModel.facultyId = this.createForm.value.faculty
    this.createModel.specializationId = this.createForm.value.specialization
    this.createModel.educationalProgramsTypeId = this.createForm.value.educationalProgramsType

    this.service.create(this.createModel)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Створення освітньої програми',
          success: 'Освітня програма створена успішно',
          error: 'Помилка створення',
        }
      )
    )
    .subscribe({
      next: async (res: ApiResponse<Guid>) => {
        await new Promise(f => setTimeout(f, 1000));
        this.router.navigate([`educational-program/${res.result}`])
      },
      error: (response: HttpErrorResponse) => {
        this.createForm.enable()
        this.error = response.error?.errors[0]
      }
    })
  }

  capitalize(input: string) {
    return input.charAt(0).toUpperCase() + input.slice(1);
  }
}
