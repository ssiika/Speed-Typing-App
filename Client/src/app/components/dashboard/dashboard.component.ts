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


  ngOnInit(): void {
    if (!this.authService.userCheck()) {
      this.router.navigate(['/login'])
    };
  }
}
