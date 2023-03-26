import { Component, OnInit } from '@angular/core';
import { Person } from '../models/person.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from '../services/account.service';
import { Guid } from 'guid-typescript';
import { ApiResponse } from '../models/api-response.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {

  person: Person;
  private jwtHelper: JwtHelperService = new JwtHelperService()
  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    if(this.isAuthenticated()){
      this.loadAuthorizedPerson()
    }
  }

  isAuthenticated(): boolean {
    const token = this.getToken();

    if(token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }

    return false;
  }

  getToken(): string | null {
    return localStorage.getItem("token");
  }

  isAdmin(): boolean {
    if(this.person)
    {
      return this.person.isAdmin
    }
    return false;
  }

  getPersonById(id: Guid){
    this.accountService.getPersonById(id).subscribe({
      next: (response: ApiResponse<Person>) => {
        console.log(response.result)
      }
    });
  }

  loadAuthorizedPerson() {
    this.accountService.getAuthorizedPerson().subscribe({
      next: (response: ApiResponse<Person>) => {
        this.person = response.result
      }
    });
  }

  logOut(){
    this.accountService.logOut().subscribe({
      next: () => {
        localStorage.removeItem('token');
        this.router.navigate(['/'])
      }
    })
  }
}
