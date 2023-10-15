import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ScheduleInfo } from 'src/app/models/scheduleResponse.module';

@Component({
  selector: 'app-snapshot',
  templateUrl: './snapshot.component.html',
  styleUrls: ['./snapshot.component.css']
})
export class SnapshotComponent {

  scheduleInfo: ScheduleInfo | undefined;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const scheduleDetails = params.get('scheduleDetails');
      if(scheduleDetails) {
        this.scheduleInfo = JSON.parse(scheduleDetails);
        console.log(this.scheduleInfo);
      }
      else {
        this.router.navigate(['students']);
      }
    });
  };
}
