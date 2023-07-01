import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentsListComponent } from './components/students/students-list/students-list.component';
import { AddStudentComponent } from './components/students/add-student/add-student.component';
import { EditStudentComponent } from './components/students/edit-student/edit-student.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { LogicComponent } from './components/auth/logic/logic.component';

const routes: Routes = [
  {
    path: '',
    component: LogicComponent
  },
  {
    path: 'login',
    component: LogicComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'students',
    component: StudentsListComponent
  },
  {
    path: 'students/add',
    component: AddStudentComponent
  },
  {
    path: 'students/edit/:id',
    component: EditStudentComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
