import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JournalRequest } from 'src/app/models/journalRequest.model';

@Component({
  selector: 'app-export',
  templateUrl: './export.component.html',
  styleUrls: ['./export.component.css']
})
export class ExportComponent {

  constructor(private route: ActivatedRoute, private router: Router) {}

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

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const subjectData = params.get('subjectData');
      if(subjectData) {
        this.journalRequest = JSON.parse(subjectData);
        console.log(this.journalRequest);
      }
      else {
        this.router.navigate(['journal']);
      }
    });
   }
}
