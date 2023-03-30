import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { AccountComponent } from '../account/account.component';
import { ApiResponse } from '../models/api-response.model';
import { Person } from '../models/person.model';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.scss']
})
export class PersonListComponent implements OnInit {

  constructor(private service: AccountService, public account: AccountComponent, private router: Router,
    private toastService: HotToastService) { }
  users: Person[] = []
  searchedUsers: Person[] = []
  searchEmail = ''
  searchFirstName = ''
  searchLastName = ''
  formOpened: boolean = false;

  ngOnInit(): void {
    this.toastService.loading("Завантаження")
    this.service.getAll().subscribe({
      next: (res: ApiResponse<Person[]>) => {
        this.users = res.result
        this.searchedUsers = this.users
        this.toastService.close()
        console.log(this.users)
      }
    })
  }

  search(){
    this.searchedUsers = this.users
    if(this.searchEmail != ''){
      this.searchedUsers = this.searchedUsers.filter((val) =>
        val.email.toLowerCase().includes(this.searchEmail)
      );
    }
    if(this.searchFirstName != ''){
      this.searchedUsers = this.searchedUsers.filter((val) =>
        val.firstName.toLowerCase().includes(this.searchFirstName)
      );
    }
    if(this.searchLastName != ''){
      this.searchedUsers = this.searchedUsers.filter((val) =>
        val.lastName.toLowerCase().includes(this.searchLastName)
      );
    }
  }

  openForm(){
    this.formOpened = !this.formOpened
  }
  closeForm(){
    this.formOpened = !this.formOpened
  }
}
