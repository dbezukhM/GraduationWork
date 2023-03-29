import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { AccountComponent } from 'src/app/account/account.component';
import { ApiResponse } from 'src/app/models/api-response.model';
import { SubjectDetails } from 'src/app/models/subject-details.model';
import { SubjectService } from 'src/app/services/subject.service';
import { WorkingProgramService } from 'src/app/services/working-program.service';

@Component({
  selector: 'app-subject-details',
  templateUrl: './subject-details.component.html',
  styleUrls: ['./subject-details.component.scss']
})
export class SubjectDetailsComponent implements OnInit {

  constructor(private router: ActivatedRoute, private service: SubjectService, private workingProgramService: WorkingProgramService,
     public account: AccountComponent, private toastService: HotToastService) { }

  subject: SubjectDetails

  ngOnInit(): void {
    this.toastService.loading("Завантаження")
    this.account.loadPerson()
    const id = this.router.snapshot.paramMap.get('id');
    this.service.getDetails(id!).subscribe({
      next: (result: ApiResponse<SubjectDetails>) =>{
        this.toastService.close()
        this.subject = result.result
        this.subject.competences = this.subject.competences.sort((a, b) => (a.name > b.name ? 1 : -1))
        this.subject.programResults = this.subject.programResults.sort((a, b) => (a.name > b.name ? 1 : -1))
      }
    })
  }

  downLoad(){
    this.toastService.loading("Формування шаблону робочої програми")
    this.workingProgramService.downloadFile(this.subject.id)
    .subscribe((event)=>{
      if (event.type === HttpEventType.Response) {
        this.toastService.close()
        this.toastService.success("Шаблон успішно сформований")
        this.downloadFile(event);
      }
    })
  }

  private downloadFile = (data: HttpResponse<Blob>) => {
    const downloadedFile = new Blob([data.body!], { type: data.body!.type });
    const a = document.createElement('a');
    a.setAttribute('style', 'display:none;');
    document.body.appendChild(a);
    a.download = `${this.subject.name} - шаблон робочої програми.docx`;
    a.href = URL.createObjectURL(downloadedFile);
    a.target = '_blank';
    a.click();
    document.body.removeChild(a);
}
}
