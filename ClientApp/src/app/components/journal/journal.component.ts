import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { JournalRequest } from 'src/app/models/journalRequest.model';
import { SubjectRequest } from 'src/app/models/subjectRequest.model';
import { SubjectResponse } from 'src/app/models/subjectResponse.module';
import { ScheduleService } from 'src/app/services/schedule/schedule.service';

@Component({
  selector: 'app-journal',
  templateUrl: './journal.component.html',
  styleUrls: ['./journal.component.css']
})
export class JournalComponent {

  subjectRequest: SubjectRequest = {
    group: '',
    date: new Date()
  }

  journalRequest: JournalRequest = {
    course: 1,
    group: '',
    date: new Date(),
    subject: 
    { 
      subjectName: '', 
      timeStart: '' 
    }
  };

  subjects: SubjectResponse[] | undefined;

  ngOnInit(): void { }
  
  constructor(private scheduleService: ScheduleService, private router: Router, private toastr: ToastrService) {}

  getSubjects() {
    if(this.subjectRequest.group === '') {
      return;
    }
    this.scheduleService.getSubject(this.subjectRequest).subscribe({
      next: (data) => {
        this.subjects = data;
        this.toastr.info('The schedule has been successfully retrived');
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

  getJournal() {
    const dataToPass = {
      ...this.journalRequest,
      date: this.journalRequest.date.toLocaleString()
    };
    this.router.navigate(['/export', { subjectData: JSON.stringify(dataToPass) }])
  }
}
