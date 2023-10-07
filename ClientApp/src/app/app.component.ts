import { Component, DoCheck } from '@angular/core';
import { AuthService } from './services/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements DoCheck {

  title = 'ClientApp';

  ngDoCheck() {}
  constructor(public authService: AuthService) { }
}

