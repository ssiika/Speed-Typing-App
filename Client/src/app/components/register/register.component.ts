import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder } from '@angular/forms';
import { AuthService } from '../../services/authService/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (this.authService.userCheck()) {
      this.router.navigate(['/'])
    };
  }
}
