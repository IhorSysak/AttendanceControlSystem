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
import { LogicComponent } from './components/auth/logic/logic.component';
import { AuthInterceptor } from './services/authInterceptor/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    StudentsListComponent,
    AddStudentComponent,
    EditStudentComponent,
    RegisterComponent,
    LogicComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
