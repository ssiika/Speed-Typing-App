import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { AuthService } from '../../services/authService/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  isStarted: boolean = false;

  initGame(): void {
    this.isStarted = true;
  }

  ngOnInit(): void {
    if (!this.authService.getValidUsername()) {
      this.router.navigate(['/login'])
    };
  }
}
