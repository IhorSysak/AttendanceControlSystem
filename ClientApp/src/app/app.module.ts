import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StudentsListComponent } from './components/students/students-list/students-list.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AddStudentComponent } from './components/students/add-student/add-student.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { ScheduleComponent } from './components/schedule/schedule.component';
import { SnapshotComponent } from './components/snapshot/snapshot.component';
import { JournalComponent } from './components/journal/journal.component';
import { MatSelectModule } from '@angular/material/select';
import { ExportComponent } from './components/export/export.component';
import { MatTableModule } from '@angular/material/table'
import { ToastrModule } from 'ngx-toastr';

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
    AttendanceTrackingComponent,
    ScheduleComponent,
    SnapshotComponent,
    JournalComponent,
    ExportComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    MatSelectModule,
    MatTableModule,
    ToastrModule.forRoot({
      tapToDismiss: true,
      newestOnTop: true,
      timeOut: 3000,
      easeTime: 300
    }),
    ReactiveFormsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
