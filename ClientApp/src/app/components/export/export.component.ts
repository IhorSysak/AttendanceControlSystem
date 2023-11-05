import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JournalRequest } from 'src/app/models/journalRequest.model';
import { JournalResponse } from 'src/app/models/journalResponse.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { ScheduleService } from 'src/app/services/schedule/schedule.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-export',
  templateUrl: './export.component.html',
  styleUrls: ['./export.component.css']
})
export class ExportComponent {

  constructor(private scheduleService: ScheduleService, private route: ActivatedRoute, private router: Router, public authService: AuthService) {}

  displayedColumns: string[] = ['position', 'lastName', 'firstName', 'middleName', 'isPresent'];

  journalRequest: JournalRequest = {
    course: 0,
    group: '',
    date: new Date(),
    subject:
    { 
      subjectName: '', 
      timeStart: '' 
    }
  };

  jornalResponse: JournalResponse = {
    course: 0,
    group: '',
    subjectName: '',
    studentPresenceInfos: []
  };

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const subjectData = params.get('subjectData');
      if(subjectData) {
        this.journalRequest = JSON.parse(subjectData);
      }
      else {
        this.router.navigate(['journal']);
      }

      this.scheduleService.getJournal(this.journalRequest).subscribe({
        next: (data) => {
          this.jornalResponse = data;
          console.log(this.jornalResponse);
        },
        error: (response) => {
          console.log(response);
        }
      });
    });
   }

  exportToExcel(): void {
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.jornalResponse.studentPresenceInfos);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();

    const columnWidths = [
      { wch: 8 },
      { wch: 15 },
      { wch: 15 },
      { wch: 20 },
      { wch: 8 },
    ];

    ws['!cols'] = columnWidths;

    XLSX.utils.book_append_sheet(wb, ws, `${this.jornalResponse.group}`);
    XLSX.writeFile(wb, `Присутність_${this.jornalResponse.group}.xlsx`);
  }
}
