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
  {path: 'users', component: PersonListComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
