import { Component } from '@angular/core';
import { Record } from '../../types';
import { Router } from "@angular/router";
import { AuthService } from '../../services/authService/auth.service';
import { RecordService } from '../../services/recordService/record.service';

@Component({
  selector: 'app-records',
  templateUrl: './records.component.html',
  styleUrls: ['./records.component.css']
})
export class RecordsComponent {
  constructor(
    private recordService: RecordService,
    private authService: AuthService,
    private router: Router
  ) { }

  records: Record[] = [];

  getRecords(): void {
    this.recordService.getRecords()
      .subscribe(records => {
        if (records.data) {
          this.records = records.data
        }      
      });
  }

  ngOnInit(): void {
    if (!this.authService.getValidUsername()) {
      this.router.navigate(['/login'])
      return;
    };
    this.getRecords();
  }
}
