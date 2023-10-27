import { Component, ElementRef, QueryList, ViewChildren } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  constructor(public authService: AuthService) {}

  setHeaderStyle(routerLink: string) {
    const elements = document.querySelectorAll('.nav-link');

    elements?.forEach(element => {
      element.classList.remove('active');
    });

    const currentElement = document.querySelector(`a.nav-link[routerLink="${routerLink}"]`);
    currentElement?.classList.add('active');
  }
}
