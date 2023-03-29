import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { Guid } from 'guid-typescript';
import { AccountComponent } from 'src/app/account/account.component';
import { ApiResponse, ServerError } from 'src/app/models/api-response.model';
import { Competence } from 'src/app/models/competence.model';
import { IdNameModel } from 'src/app/models/id-name-model.model';
import { ProgramResult } from 'src/app/models/program-result.model';
import { SubjectDetails } from 'src/app/models/subject-details.model';
import { SubjectUpdate } from 'src/app/models/subject-update.model';
import { LookupService } from 'src/app/services/lookup.service';
import { SubjectService } from 'src/app/services/subject.service';
import { WorkingProgramService } from 'src/app/services/working-program.service';

@Component({
  selector: 'app-subject-update',
  templateUrl: './subject-update.component.html',
  styleUrls: ['./subject-update.component.scss']
})
export class SubjectUpdateComponent implements OnInit {

  constructor(private router: ActivatedRoute, private service: SubjectService, private workingProgramService: WorkingProgramService,
    public account: AccountComponent, private toastService: HotToastService, private lookupService: LookupService,
    private navigationRouter: Router) { }

  error: ServerError | null;
  subject: SubjectDetails
  updateModel: SubjectUpdate = {} as SubjectUpdate
  selectiveBlocks: IdNameModel[]
  finalControlTypes: IdNameModel[]
  competences: Competence[]
  programResults: ProgramResult[]
  selectedCompetences: Competence[] = []
  selectedCompetence: Competence | null = null
  selectedProgramResults: ProgramResult[] = []
  selectedProgramResult: ProgramResult | null = null
  disabled: boolean = false

  ngOnInit(): void {
    this.initializeFields()
  }

  back(){
    this.navigationRouter.navigate([`/subject/${this.subject.id}`])
  }

  initializeFields(){
    this.toastService.loading("Завантаження")
    this.account.loadPerson()
    const id = this.router.snapshot.paramMap.get('id');
    this.service.getDetails(id!).subscribe({
      next: (result: ApiResponse<SubjectDetails>) =>{
        this.toastService.close()
        this.subject = result.result
        this.subject.competences = this.subject.competences.sort((a, b) => (a.name > b.name ? 1 : -1))
        this.subject.programResults = this.subject.programResults.sort((a, b) => (a.name > b.name ? 1 : -1))
        this.mapModels(this.subject)
        this.loadAdditionalFields()
      }
    })
  }
  mapModels(model: SubjectDetails){
    this.updateModel.name = this.subject.name
    this.updateModel.credits = this.subject.credits
    this.updateModel.semester = this.subject.semester
    this.updateModel.lecturesHours = this.subject.lecturesHours
    this.updateModel.seminarsHours = this.subject.seminarsHours
    this.updateModel.practicalClassesHours = this.subject.practicalClassesHours
    this.updateModel.laboratoryClassesHours = this.subject.laboratoryClassesHours
    this.updateModel.trainingsHours = this.subject.trainingsHours
    this.updateModel.consultationsHours = this.subject.consultationsHours
    this.updateModel.selfWorkHours = this.subject.selfWorkHours
    this.updateModel.selectiveBlockId = this.subject.selectiveBlock.id
    this.updateModel.finalControlTypeId = this.subject.finalControlType.id
  }

  loadAdditionalFields(){
    this.lookupService.getSelectiveBlocks().subscribe({
      next: (result: ApiResponse<IdNameModel[]>) => {
        this.selectiveBlocks = result.result
      }
    })

    this.lookupService.getFinalControlTypes().subscribe({
      next: (result: ApiResponse<IdNameModel[]>) => {
        this.finalControlTypes = result.result
      }
    })

    this.lookupService.getCompetences(this.subject.educationalProgram.id).subscribe({
      next: (result: ApiResponse<Competence[]>) => {
        this.competences = result.result
        var ids = this.subject.competences.map(({ id }) => id)
        this.selectedCompetences = [...this.competences.filter(x => ids.includes(x.id))]
        this.competences = [...this.competences.filter(x => !ids.includes(x.id))]
      }
    })

    this.lookupService.getProgramResults(this.subject.educationalProgram.id).subscribe({
      next: (result: ApiResponse<ProgramResult[]>) => {
        this.programResults = result.result
        var ids = this.subject.programResults.map(({ id }) => id)
        this.selectedProgramResults = [...this.programResults.filter(x => ids.includes(x.id))]
        this.programResults = [...this.programResults.filter(x => !ids.includes(x.id))]
      }
    })
  }

  deleteCompetense(id: Guid){
    var comp = this.selectedCompetences.filter(x => x.id == id)
    this.competences.push(comp[0])
    this.selectedCompetences = this.selectedCompetences.filter(x => x.id != id)
  }

  addCompetence(comp: Competence){
    this.selectedCompetences.push(comp)
    this.competences = [...this.competences.filter(x => x.id != comp.id)]
    this.selectedCompetence = null
  }

  deleteProgramResult(id: Guid){
    var res = this.selectedProgramResults.filter(x => x.id == id)
    this.programResults.push(res[0])
    this.selectedProgramResults = this.selectedProgramResults.filter(x => x.id != id)
  }

  addProgramResult(res: ProgramResult){
    this.selectedProgramResults.push(res)
    this.programResults = [...this.programResults.filter(x => x.id != res.id)]
    this.selectedProgramResult = null
  }

  updateSubject(){
    this.disabled = true
    this.error = null
    this.updateModel.competencesIds = this.selectedCompetences.map(({ id }) => id)
    this.updateModel.programResultsIds = this.selectedProgramResults.map(({ id }) => id)
    this.service.update(this.subject.id, this.updateModel)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Оновлення дисципліни',
          success: 'Дисципліна оновлена успішно',
          error: 'Помилка оновлення',
        }
      )
    )
    .subscribe({
      next: async () => {
        await new Promise(f => setTimeout(f, 1000));
        this.navigationRouter.navigate([`subject/${this.subject.id}`])
      },
      error: (response: HttpErrorResponse) => {
        this.disabled = false
        this.error = response.error?.errors[0]
      }
    })
  }
}
