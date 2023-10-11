import { Component, OnInit } from '@angular/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { Schedule } from 'src/app/models/schedule.model';

@Component({
  selector: 'app-attendance-tracking',
  templateUrl: './attendance-tracking.component.html',
  styleUrls: ['./attendance-tracking.component.css']
})
export class AttendanceTrackingComponent implements OnInit {
  schedule: Schedule = {
    course: 1,
    fullName: '',
    group: '',
    date: new Date()
  }

  ngOnInit(): void { }
  
  constructor() {}

  getSchedule(): void {
    console.log(this.schedule);
  }
}
