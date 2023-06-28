import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StudentsListComponent } from './components/students/students-list/students-list.component';
import { HttpClientModule } from '@angular/common/http';
import { AddStudentComponent } from './components/students/add-student/add-student.component';
import { FormsModule } from '@angular/forms';
import { EditStudentComponent } from './components/students/edit-student/edit-student.component';

@NgModule({
  declarations: [
    AppComponent,
    StudentsListComponent,
    AddStudentComponent,
    EditStudentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
