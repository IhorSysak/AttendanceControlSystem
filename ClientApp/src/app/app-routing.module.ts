import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentsListComponent } from './components/students/students-list/students-list.component';
import { AddStudentComponent } from './components/students/add-student/add-student.component';
import { EditStudentComponent } from './components/students/edit-student/edit-student.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { LoginComponent } from './components/auth/login/login.component';
import { TeachersListComponent } from './components/teachers/teachers-list/teachers-list.component';
import { AddTeacherComponent } from './components/teachers/add-teacher/add-teacher.component';
import { AuthGuardService } from './services/auth/authGuard/auth-guard.service';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';
import { AttendanceTrackingComponent } from './components/attendance-tracking/attendance-tracking.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { SnapshotComponent } from './components/snapshot/snapshot.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'students', component: StudentsListComponent },
  { path: 'students/add', component: AddStudentComponent },
  { path: 'students/edit/:id', component: EditStudentComponent },
  { path: 'teachers', component: TeachersListComponent, canActivate:[AuthGuardService], data:{roles:['Admin']} },
  { path: 'teacher/add', component: AddTeacherComponent },
  { path: 'forbidden', component: ForbiddenComponent },
  { path: 'attendanceTracking', component: AttendanceTrackingComponent },
  { path: 'schedule', component: ScheduleComponent },
  { path: 'snapshot', component: SnapshotComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
