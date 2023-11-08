import { Component } from '@angular/core';
import { Record } from '../../types';
import { RecordService } from '../../services/recordService/record.service';

@Component({
  selector: 'app-records',
  templateUrl: './records.component.html',
  styleUrls: ['./records.component.css']
})
export class RecordsComponent {
  records: Record[] = [];
  asyncRecords: Record[] = [];


  constructor(private recordService: RecordService) { }

  log(val: any) { console.log(val); }

  ngOnInit(): void {
    this.getAsyncRecords();
  }


  getAsyncRecords(): void {
    this.recordService.getAsyncRecords()
      .subscribe(records => {
        console.log(records)
        this.asyncRecords = records.data
      });
  }
}
