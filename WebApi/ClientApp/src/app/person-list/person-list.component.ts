import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { AccountComponent } from '../account/account.component';
import { ApiResponse, ServerError } from '../models/api-response.model';
import { Person } from '../models/person.model';
import { AccountService } from '../services/account.service';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PersonCreate } from '../models/person-create.model';
import { Guid } from 'guid-typescript';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.scss']
})
export class PersonListComponent implements OnInit {

  constructor(private service: AccountService, public account: AccountComponent, private router: Router,
    private toastService: HotToastService, private formBuilder: FormBuilder) { }
  users: Person[] = []
  searchedUsers: Person[] = []
  searchEmail = ''
  searchFirstName = ''
  searchLastName = ''
  formOpened: boolean = false;
  rolesNames: Array<any> = [
    {name: "Admin", value: "Адміністратор"},
    {name: "Lecturer", value: "Викладач"}
  ]
  createForm: FormGroup
  error: ServerError | null;
  model: PersonCreate = {} as PersonCreate

  page: number = 1;
  tableSize: number = 7;
  onTableDataChange(event: any){
    this.page = event
  }

  ngOnInit(): void {
    this.loadData()
    this.createForm = this.formBuilder.group({
      email: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      roles: this.formBuilder.array([], [Validators.required]),
    })
  }

  loadData(){
    this.toastService.loading("Завантаження")
    this.service.getAll().subscribe({
      next: (res: ApiResponse<Person[]>) => {
        this.users = res.result
        this.searchedUsers = this.users
        this.toastService.close()
      }
    })
  }

  onCheckboxChange(e: any) {
    var check: FormArray = this.createForm.get('roles') as FormArray;
    if (e.target.checked) {
      check.push(new FormControl(e.target.value));
    } else {
      let i: number = 0;
      check.controls.forEach((item: any) => {
        if (item.value == e.target.value) {
          check.removeAt(i);
          return;
        }
        i++;
      });
    }
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
    this.createForm.reset()
    this.formOpened = !this.formOpened
  }
  closeForm(){
    this.formOpened = !this.formOpened
  }

  submit(){
    this.createForm.disable()
    this.error = null
    this.model.email = this.createForm.value.email
    this.model.firstName = this.createForm.value.firstName
    this.model.lastName = this.createForm.value.lastName
    this.model.roles = this.createForm.value.roles
    this.service.createPerson(this.model)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Створення акаунту користувача',
          success: 'Акаунт створено успішно',
          error: 'Помилка створення',
        }
      )
    )
    .subscribe({
      next: async (res: ApiResponse<Guid>) => {
        await new Promise(f => setTimeout(f, 1000));
        this.router.navigate([`user/${res.result}`])
      },
      error: (response: HttpErrorResponse) => {
        this.createForm.enable()
        this.error = response.error?.errors[0]
      }
    })
  }
}
