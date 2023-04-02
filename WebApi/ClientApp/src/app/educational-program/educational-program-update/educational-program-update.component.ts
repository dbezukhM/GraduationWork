import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { AccountComponent } from 'src/app/account/account.component';
import { ApiResponse, ServerError } from 'src/app/models/api-response.model';
import { EducationalProgramDetails } from 'src/app/models/educational-program-details.model';
import { EducationalProgramUpdate } from 'src/app/models/educational-program-update.model';
import { IdNameModel } from 'src/app/models/id-name-model.model';
import { EducationalProgramService } from 'src/app/services/educational-program.service';
import { LookupService } from 'src/app/services/lookup.service';

@Component({
  selector: 'app-educational-program-update',
  templateUrl: './educational-program-update.component.html',
  styleUrls: ['./educational-program-update.component.scss']
})
export class EducationalProgramUpdateComponent implements OnInit {

  constructor(private activatedRouter: ActivatedRoute, private service: EducationalProgramService, private lookUp: LookupService,
    private formBuilder: FormBuilder, private router: Router, private toastService: HotToastService) { }

  educationalProgram: EducationalProgramDetails;
  educationalProgramUpdate: EducationalProgramUpdate = {} as EducationalProgramUpdate

  updateForm: FormGroup;
  faculties: IdNameModel[] = []
  specializations: IdNameModel[] = []
  educationalProgramsTypes: IdNameModel[] = []
  areaOfExpertiselistlist: IdNameModel[] = []
  universities: IdNameModel[] = []
  error: ServerError | null;

  ngOnInit(): void {
    this.loadData()
  }

  submit(){
    this.error = null
    this.updateForm.disable()
    this.educationalProgramUpdate.name = this.updateForm.value.name
    this.educationalProgramUpdate.facultyId = this.updateForm.value.faculty
    this.educationalProgramUpdate.specializationId = this.updateForm.value.specialization
    this.educationalProgramUpdate.educationalProgramsTypeId = this.updateForm.value.educationalProgramsType
    console.log(this.educationalProgramUpdate)
    this.service.update(this.educationalProgram.id, this.educationalProgramUpdate)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Оновлення освітньої програми',
          success: 'Освітня програма оновлена успішно',
          error: 'Помилка оновлення',
        }
      )
    )
    .subscribe({
      next: async () => {
        await new Promise(f => setTimeout(f, 1000));
        this.router.navigate([`educational-program/${this.educationalProgram.id}`])
      },
      error: (response: HttpErrorResponse) => {
        this.updateForm.enable()
        this.error = response.error?.errors[0]
      }
    })
  }

  loadData(){
    const id = this.activatedRouter.snapshot.paramMap.get('id');
    this.service.getDetails(id!).subscribe({
      next: (res: ApiResponse<EducationalProgramDetails>) => {
        this.educationalProgram = res.result
        this.loadAdditionalData()
        
        this.updateForm = this.formBuilder.group({
          name: [this.educationalProgram.name, Validators.required],
          areaOfExpertise: [this.educationalProgram.areaOfExpertise.id, Validators.required],
          specialization: [this.educationalProgram.specialization.id, Validators.required],
          university: [this.educationalProgram.university.id, Validators.required],
          faculty: [this.educationalProgram.faculty.id, Validators.required],
          educationalProgramsType: [this.educationalProgram.educationalProgramsType.id, Validators.required],
        })
      }
    })

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

  loadAdditionalData(){
    this.lookUp.getFaculties(this.educationalProgram.university.id).subscribe({
      next: (res: ApiResponse<IdNameModel[]>) => {
        this.faculties = res.result.map(x => {
          x.name = this.capitalize(x.name)
          return x;
        })
      }
    })
    this.lookUp.getSpecializations(this.educationalProgram.areaOfExpertise.id).subscribe({
      next: (res: ApiResponse<IdNameModel[]>) => {
        this.specializations = res.result.map(x => {
          x.name = this.capitalize(x.name)
          return x;
        })
      }
    })
  }

  onUniversityChange(event: any){
    var universityId = this.updateForm.value.university;
    this.lookUp.getFaculties(universityId).subscribe({
      next: (res: ApiResponse<IdNameModel[]>) => {
        this.faculties = res.result.map(x => {
          x.name = this.capitalize(x.name)
          return x;
        })
        this.updateForm.get('faculty')?.setValue(this.faculties[0].id)
      }
    })
  }

  onAreaOfExpertiseChange(event: any){
    var areaOfExpertiseId = this.updateForm.value.areaOfExpertise;
    this.lookUp.getSpecializations(areaOfExpertiseId).subscribe({
      next: (res: ApiResponse<IdNameModel[]>) => {
        this.specializations = res.result.map(x => {
          x.name = this.capitalize(x.name)
          return x;
        })
        this.updateForm.get('specialization')?.setValue(this.specializations[0].id)
      }
    })
  }

  back(){
    this.router.navigate([`educational-program/${this.educationalProgram.id}`])
  }

  capitalize(input: string) {
    return input.charAt(0).toUpperCase() + input.slice(1);
  }

}
