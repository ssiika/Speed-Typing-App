import { Component } from '@angular/core';
import { Router, NavigationEnd } from "@angular/router";
import { AuthService } from '../../services/authService/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent {
  username: string = '';
  constructor(
    private router: Router,
    private authService: AuthService
  ) {
    // Update username when route changes
    this.router.events.subscribe((ev) => {
      if (ev instanceof NavigationEnd) {
        this.username = this.authService.getValidUsername();
      }
    })
  }

  logout(): void {
    localStorage.removeItem('user')
    this.router.navigate(['/login'])
  }

  ngOnInit(): void {
      this.username = this.authService.getValidUsername();
  }
}
