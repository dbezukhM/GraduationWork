import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormBuilder, FormsModule, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from "@angular/material/icon";
import {NgxPaginationModule} from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HotToastModule } from '@ngneat/hot-toast';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BodyComponent } from './body/body.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { AccountComponent } from './account/account.component';
import { AuthTokenInterceptor } from './guards/auth-token.interceptor';
import { SubjectListComponent } from './subject/subject-list/subject-list.component';
import { SubjectDetailsComponent } from './subject/subject-details/subject-details.component';
import { SubjectUpdateComponent } from './subject/subject-update/subject-update.component';
import { SubjectCreateComponent } from './subject/subject-create/subject-create.component';
import { WorkingProgramListComponent } from './working-program/working-program-list/working-program-list.component';
import { WorkingProgramDetailsComponent } from './working-program/working-program-details/working-program-details.component';
import { PersonListComponent } from './person-list/person-list.component';
import { PersonDetailsComponent } from './person-list/person-details/person-details.component';
import { EducationalProgramListComponent } from './educational-program/educational-program-list/educational-program-list.component';
import { EducationalProgramDetailsComponent } from './educational-program/educational-program-details/educational-program-details.component';
import { EducationalProgramCreateComponent } from './educational-program/educational-program-create/educational-program-create.component';
import { EducationalProgramUpdateComponent } from './educational-program/educational-program-update/educational-program-update.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogService } from './services/confirmation-dialog.service';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    BodyComponent,
    LoginComponent,
    AccountComponent,
    SubjectListComponent,
    SubjectDetailsComponent,
    SubjectUpdateComponent,
    SubjectCreateComponent,
    WorkingProgramListComponent,
    WorkingProgramDetailsComponent,
    PersonListComponent,
    PersonDetailsComponent,
    EducationalProgramListComponent,
    EducationalProgramDetailsComponent,
    EducationalProgramCreateComponent,
    EducationalProgramUpdateComponent,
    ConfirmationDialogComponent
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
    MatIconModule,
    NgxPaginationModule
  ],
  providers: [FormBuilder, AuthGuard,
  {
    provide: HTTP_INTERCEPTORS,
    multi: true,
    useClass: AuthTokenInterceptor
  }, AccountComponent, ConfirmationDialogService],
  bootstrap: [AppComponent]
})
export class AppModule { }
