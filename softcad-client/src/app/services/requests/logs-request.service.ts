import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Log } from '../../models/log';

@Injectable({
  providedIn: 'root'
})
export class LogsRequestService {

  private readonly API = `https://localhost:7032/logs`;
  changed = new Subject<void>();

  constructor(private http: HttpClient) { }

  getLogs(): Observable<Log[]> {
    return this.http.get<Log[]>(this.API);
  }
}