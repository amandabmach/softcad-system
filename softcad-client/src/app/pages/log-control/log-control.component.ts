import { Log } from '../../models/log';
import { Component, OnInit } from '@angular/core';
import { LogsRequestService } from '../../services/requests/logs-request.service';
import { format } from 'date-fns';

@Component({
  selector: 'app-log-control',
  templateUrl: './log-control.component.html',
  styleUrl: './log-control.component.scss'
})
export class LogControlComponent implements OnInit{

  visibility: boolean = true;
  resultado!: string;
  responsavel!: string;
  operation!: string;
  log!: Log;
  data!: Log[];

  modal!: boolean;

  constructor(private service: LogsRequestService) { }

  ngOnInit(): void {
    this.getLogs();
  }

  getLogs() {
    this.service.getLogs().subscribe(logs => {
      logs = logs.map(log => {
        const timestampFormatado = format(new Date(log.timestamp), 'dd/MM/yyyy HH:mm:ss');
        return { ...log, timestamp: timestampFormatado };
      });
      this.data = logs;
    })
  }
  
  logClicked(log: any) {
    this.log = log;
    this.modal = true;  
  }
}
