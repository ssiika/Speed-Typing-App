import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private formBuilder: FormBuilder,
  ) { }

  message: string = '';

  loginForm = this.formBuilder.group({
    username: '',
    password: ''
  });

  onSubmit(): void {
    const userData = {
      username: this.loginForm.value.username?.trim(),
      password: this.loginForm.value.password?.trim(),
    }
    this.message = `${this.loginForm.value.username}: ${this.loginForm.value.password}`;
    this.loginForm.reset();
  }
}
