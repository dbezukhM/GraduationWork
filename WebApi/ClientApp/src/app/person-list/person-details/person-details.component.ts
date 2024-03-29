import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { ApiResponse } from 'src/app/models/api-response.model';
import { Person } from 'src/app/models/person.model';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-person-details',
  templateUrl: './person-details.component.html',
  styleUrls: ['./person-details.component.scss']
})
export class PersonDetailsComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router, private activatedRouter: ActivatedRoute) { }
  person: Person;
  pageAut: number = 1;
  pageApprover: number = 1;
  tableSize: number = 5;
  isAdminP: boolean = false;
  isMethodistP: boolean = false;
  isLecturerP: boolean = false;

  onTableAutDataChange(event: any){
    this.pageAut = event
  }

  onTableApproverDataChange(event: any){
    this.pageApprover = event
  }

  ngOnInit(): void {
    const id = this.activatedRouter.snapshot.paramMap.get('id');
    this.getPersonById(id!)
  }

  getPersonById(id: string){
    this.accountService.getPersonById(id).subscribe({
      next: (response: ApiResponse<Person>) => {
        this.person = response.result
        if(this.person.roles.includes("Admin")){
          this.isAdminP = true;
        }
        if(this.person.roles.includes("Lecturer")){
          this.isLecturerP = true;
        }
        if(this.person.roles.includes("Methodist")){
          this.isMethodistP = true;
        }
      }
    });
  }
}
