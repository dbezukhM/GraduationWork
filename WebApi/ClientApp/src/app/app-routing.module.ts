import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { PersonListComponent } from './person-list/person-list.component';
import { SubjectCreateComponent } from './subject/subject-create/subject-create.component';
import { SubjectDetailsComponent } from './subject/subject-details/subject-details.component';
import { SubjectListComponent } from './subject/subject-list/subject-list.component';
import { SubjectUpdateComponent } from './subject/subject-update/subject-update.component';
import { WorkingProgramDetailsComponent } from './working-program/working-program-details/working-program-details.component';
import { WorkingProgramListComponent } from './working-program/working-program-list/working-program-list.component';
import { PersonDetailsComponent } from './person-list/person-details/person-details.component';
import { EducationalProgramListComponent } from './educational-program/educational-program-list/educational-program-list.component';
import { EducationalProgramDetailsComponent } from './educational-program/educational-program-details/educational-program-details.component';
import { EducationalProgramCreateComponent } from './educational-program/educational-program-create/educational-program-create.component';
import { EducationalProgramUpdateComponent } from './educational-program/educational-program-update/educational-program-update.component';

const routes: Routes = [
  {path: '', component: SubjectListComponent},
  {path: 'login', component: LoginComponent},
  {path: 'account', component: AccountComponent, canActivate: [AuthGuard]},
  {path: 'subjects', component: SubjectListComponent},
  {path: 'subject/:id', component: SubjectDetailsComponent},
  {path: 'subject-update/:id', component: SubjectUpdateComponent, canActivate: [AuthGuard]},
  {path: 'subject-create', component: SubjectCreateComponent, canActivate: [AuthGuard]},
  {path: 'working-programs', component: WorkingProgramListComponent},
  {path: 'working-program/:id', component: WorkingProgramDetailsComponent},
  {path: 'users', component: PersonListComponent, canActivate: [AuthGuard]},
  {path: 'user/:id', component: PersonDetailsComponent, canActivate: [AuthGuard]},
  {path: 'educational-programs', component: EducationalProgramListComponent},
  {path: 'educational-program/:id', component: EducationalProgramDetailsComponent},
  {path: 'educational-program-create', component: EducationalProgramCreateComponent, canActivate: [AuthGuard]},
  {path: 'educational-program-update/:id', component: EducationalProgramUpdateComponent, canActivate: [AuthGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
