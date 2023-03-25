import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticatedResponse } from '../models/authenticated-response.model';
import { ApiResponse, ServerError } from '../models/api-response.model';
import { HttpErrorResponse } from '@angular/common/http';
import { HotToastService } from '@ngneat/hot-toast';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router,
    private formBuilder: FormBuilder, private toastService: HotToastService) { }

  loginForm: FormGroup = this.formBuilder.group({
    Email: ['', [Validators.required, Validators.email]],
    Password: ['', Validators.required]
  });
  error: ServerError | null;
  
  ngOnInit(): void {
  }
  login(){
    this.loginForm.disable()
    this.error = null
    this.accountService.login(this.loginForm.value.Email, this.loginForm.value.Password)
    .pipe(
      this.toastService.observe(
        {
          loading: 'Авторизація до системи',
          success: 'Успішна авторизація',
          error: 'Помилка авторизації',
        }
      )
    )
    .subscribe({
      next: (response: ApiResponse<AuthenticatedResponse>) => {
        localStorage.setItem("token", response.result.token); 
      },
      error: (response: HttpErrorResponse) => {
        this.loginForm.enable()
        this.error = response.error?.errors[0]
      }
    })
  }
}
