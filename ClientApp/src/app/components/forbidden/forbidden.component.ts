import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-forbidden',
  templateUrl: './forbidden.component.html',
  styleUrls: ['./forbidden.component.css']
})
export class ForbiddenComponent {

  ngOnInit(): void { }

  constructor(private location: Location) {}

  goBack() {
    this.location.back();
  }
}
