import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ScheduleInfo, ScheduleResponse } from 'src/app/models/scheduleResponse.module';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent {

  constructor(private route: ActivatedRoute, private router: Router) {}

  scheduleResponse: ScheduleResponse = {
    dayOfWeek: '',
    scheduleInfos: []
  }

  dayOfWeek: string | undefined = '';

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const scheduleData = params.get('scheduleData');
      if(scheduleData) {
        this.scheduleResponse = JSON.parse(scheduleData);
      }
      else {
        this.router.navigate(['students']);
      }

      const abbreviation = this.scheduleResponse.dayOfWeek;
      this.dayOfWeek = convertAbbreviationToUkrainianFullname(abbreviation);
    });
  };

  redirectToScheduleDetailsPage(scheduleInfo: ScheduleInfo) {
    this.router.navigate(['/snapshot', { scheduleDetails: JSON.stringify(scheduleInfo) }]);
  }
}

const WeekdayMappings: {[key : string]: string} = {
  'Пн': 'Monday',
  'Вв': 'Tuesday',
  'Ср': 'Wednesday',
  'Чт': 'Thursday',
  'Пт': 'Friday',
  'Сб': 'Saturday'
}

function convertAbbreviationToUkrainianFullname(abbreviation: string): string | undefined {
  return WeekdayMappings[abbreviation];
}
