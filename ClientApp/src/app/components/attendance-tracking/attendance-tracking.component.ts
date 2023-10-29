import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ScheduleRequest } from 'src/app/models/scheduleRequest.model';
import { ScheduleService } from 'src/app/services/schedule/schedule.service';

@Component({
  selector: 'app-attendance-tracking',
  templateUrl: './attendance-tracking.component.html',
  styleUrls: ['./attendance-tracking.component.css']
})
export class AttendanceTrackingComponent implements OnInit {
  scheduleRequest: ScheduleRequest = {
    course: 1,
    firstName: '',
    lastName: '',
    middleName: '',
    group: '',
    date: new Date()
  }

  ngOnInit(): void { }
  
  constructor(private scheduleService: ScheduleService, private router: Router) {}

  getSchedule() {
    this.scheduleService.getSchedule(this.scheduleRequest).subscribe({
      next: (data) => {
        this.router.navigate(['/schedule', { scheduleData: JSON.stringify(data) }])
      },
      error: (response) => {
        console.log(response);
      }
    })
  }
}
