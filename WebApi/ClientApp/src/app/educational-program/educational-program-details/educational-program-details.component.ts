import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountComponent } from 'src/app/account/account.component';
import { ApiResponse } from 'src/app/models/api-response.model';
import { EducationalProgramDetails } from 'src/app/models/educational-program-details.model';
import { EducationalProgramService } from 'src/app/services/educational-program.service';

@Component({
  selector: 'app-educational-program-details',
  templateUrl: './educational-program-details.component.html',
  styleUrls: ['./educational-program-details.component.scss']
})
export class EducationalProgramDetailsComponent implements OnInit {

  constructor(private activatedRouter: ActivatedRoute, private service: EducationalProgramService, private router: Router,
    public account: AccountComponent) { }

  educationalProgram: EducationalProgramDetails

  ngOnInit(): void {
    this.account.loadPerson()
    this.loadData()
  }

  loadData(){
    const id = this.activatedRouter.snapshot.paramMap.get('id');
    this.service.getDetails(id!).subscribe({
      next: (res: ApiResponse<EducationalProgramDetails>) => {
        this.educationalProgram = res.result
      }
    })
  }
}
