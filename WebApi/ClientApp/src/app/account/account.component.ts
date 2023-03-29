import { Component, OnInit } from '@angular/core';
import { Person } from '../models/person.model';
import { ChangePassword } from '../models/change-password.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from '../services/account.service';
import { Guid } from 'guid-typescript';
import { ApiResponse, ServerError } from '../models/api-response.model';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {

  person: Person;
  private jwtHelper: JwtHelperService = new JwtHelperService()
  changePasswordFormOpened: boolean = false;
  changePasswordForm: FormGroup;
  error: ServerError | null;
  req: ChangePassword = {} as ChangePassword;

  constructor(private accountService: AccountService, private router: Router, private formBuilder: FormBuilder,
    private toastService: HotToastService) { }

  ngOnInit(): void {
    this.loadPerson()
    this.changePasswordForm = this.formBuilder.group({
      OldPassword: ['', Validators.required],
      NewPassword: ['', [Validators.required]],
      ConfirmPassword: ['', Validators.required]},
       { validator: this.comparePasswords });
  }

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword')!;
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('NewPassword')!.value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  loadPerson(){
    if(this.isAuthenticated()){
      this.loadAuthorizedPerson()
    }
  }

  changePassword(){
    this.error = null
    this.req.oldPassword = this.changePasswordForm.value.OldPassword;
    this.req.newPassword = this.changePasswordForm.value.NewPassword;
    this.changePasswordForm.disable()
    this.accountService.changePassword(this.req)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Обробка',
          success: 'Успішна зміна паролю',
          error: 'Помилка',
        }
      )
    )
    .subscribe({
      next: () =>{
        this.changePasswordForm.enable()
        this.changePasswordForm.reset()
        this.changePasswordFormOpened = false
      },
      error: (response: HttpErrorResponse) => {
        this.changePasswordForm.enable()
        this.error = response.error?.errors[0]
      }
    })
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

  openChangePasswordForm(){
    this.changePasswordFormOpened = !this.changePasswordFormOpened
  }
  closeChangePasswordForm(){
    this.changePasswordFormOpened = !this.changePasswordFormOpened
  }
}
