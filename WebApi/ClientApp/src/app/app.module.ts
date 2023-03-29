import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormBuilder, FormsModule, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from "@angular/material/icon";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HotToastModule } from '@ngneat/hot-toast';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BodyComponent } from './body/body.component';
import { ProductsComponent } from './products/products.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { AccountComponent } from './account/account.component';
import { AuthTokenInterceptor } from './guards/auth-token.interceptor';
import { SubjectListComponent } from './subject/subject-list/subject-list.component';
import { SubjectDetailsComponent } from './subject/subject-details/subject-details.component';
import { SubjectUpdateComponent } from './subject/subject-update/subject-update.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    BodyComponent,
    ProductsComponent,
    LoginComponent,
    AccountComponent,
    SubjectListComponent,
    SubjectDetailsComponent,
    SubjectUpdateComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HotToastModule.forRoot(
      {
        position: 'top-right',
      }
    ),
    HttpClientModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule
  ],
  providers: [FormBuilder, AuthGuard,
  {
    provide: HTTP_INTERCEPTORS,
    multi: true,
    useClass: AuthTokenInterceptor
  }, AccountComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
