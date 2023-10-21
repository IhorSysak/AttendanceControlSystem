import { Component } from '@angular/core';
import { Router } from '@angular/router';
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
    course: 0,
    group: '',
    date: new Date(),
    subject: 
    { 
      name: '', 
      time: '' 
    }
  };

  subjects: SubjectResponse[] | undefined;

  ngOnInit(): void { }
  
  constructor(private scheduleService: ScheduleService, private router: Router) {}

  getSubjects() {
    if(this.subjectRequest.group === '') {
      return;
    }
    this.scheduleService.getSubject(this.subjectRequest).subscribe({
      next: (data) => {
        this.subjects = data;
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

  getJournal() {
    console.log(this.journalRequest);
    this.router.navigate(['/export', { subjectData: JSON.stringify(this.journalRequest) }])
  }
}
