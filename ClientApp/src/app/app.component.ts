import { Component, DoCheck, OnChanges, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements DoCheck {

  isLogin: boolean = false;

  constructor() { 
    
  }

  ngDoCheck() {
    const token = localStorage.getItem('authToken');
    token ? this.isLogin = true : this.isLogin = false;
  }
  title = 'ClientApp';
}
