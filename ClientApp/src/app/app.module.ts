import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StudentsListComponent } from './components/students/students-list/students-list.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AddStudentComponent } from './components/students/add-student/add-student.component';
import { FormsModule } from '@angular/forms';
import { EditStudentComponent } from './components/students/edit-student/edit-student.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { LoginComponent } from './components/auth/login/login.component';
import { AuthInterceptor } from './services/authInterceptor/auth.interceptor';
import { TeachersListComponent } from './components/teachers/teachers-list/teachers-list.component';
import { AddTeacherComponent } from './components/teachers/add-teacher/add-teacher.component';
import { HeaderComponent } from './components/header/header.component';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';
import { AttendanceTrackingComponent } from './components/attendance-tracking/attendance-tracking.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker'
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';

@NgModule({
  declarations: [
    AppComponent,
    StudentsListComponent,
    AddStudentComponent,
    EditStudentComponent,
    RegisterComponent,
    LoginComponent,
    TeachersListComponent,
    AddTeacherComponent,
    HeaderComponent,
    ForbiddenComponent,
    AttendanceTrackingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
