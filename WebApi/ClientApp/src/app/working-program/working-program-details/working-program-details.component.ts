import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { AccountComponent } from 'src/app/account/account.component';
import { ApiResponse } from 'src/app/models/api-response.model';
import { WorkingProgramDetails } from 'src/app/models/working-program-details.model';
import { WorkingProgramService } from 'src/app/services/working-program.service';

@Component({
  selector: 'app-working-program-details',
  templateUrl: './working-program-details.component.html',
  styleUrls: ['./working-program-details.component.scss']
})
export class WorkingProgramDetailsComponent implements OnInit {

  constructor(private activateRouter: ActivatedRoute, private service: WorkingProgramService, public account: AccountComponent,
    private toastService: HotToastService, private router: Router) { }

  model: WorkingProgramDetails

  ngOnInit(): void {
    this.loadData()
    this.account.loadPerson()
  }
  loadData(){
    const id = this.activateRouter.snapshot.paramMap.get('id');
    this.service.getDetails(id!).subscribe({
      next: (res: ApiResponse<WorkingProgramDetails>) => {
        this.model = res.result
        console.log(this.model)
      }
    })
  }
  private downloadFile = (data: HttpResponse<Blob>) => {
    const downloadedFile = new Blob([data.body!], { type: data.body!.type });
    const a = document.createElement('a');
    a.setAttribute('style', 'display:none;');
    document.body.appendChild(a);
    a.download = `${this.model.name}.docx`;
    a.href = URL.createObjectURL(downloadedFile);
    a.target = '_blank';
    a.click();
    document.body.removeChild(a);
  }
  downLoad(){
    this.toastService.loading("Завантаження файлу робочої програми")
    this.service.getWorkingProgramFile(this.model.id)
    .subscribe((event)=>{
      if (event.type === HttpEventType.Response) {
        this.toastService.close()
        this.downloadFile(event);
      }
    })
  }
  delete(){
    this.service.delete(this.model.id)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Видалення робочої програми',
          success: 'Видалення успішне',
          error: 'Помилка видалення, спробуйте пізніше',
        }
      )
    )
    .subscribe({
      next: async () => {
        await new Promise(f => setTimeout(f, 1000));
        this.router.navigate(['/working-programs'])
      },
      error: () => {
      }
    })
  }

  approve(){
    this.service.approveWp(this.model.id)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Підтвердження робочої програми',
          success: 'Підтвердження успішне',
          error: 'Помилка підтвердження, спробуйте пізніше',
        }
      )
    )
    .subscribe({
      next: ()=>{
        this.loadData()
      }
    })
  }
}
