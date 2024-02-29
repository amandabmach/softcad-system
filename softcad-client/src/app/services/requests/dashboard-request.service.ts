import { Injectable } from '@angular/core';
import { Observable, catchError, throwError} from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DashboardRequestService {

  private readonly API = `https://localhost:7032/dashboard`;

  constructor(private http: HttpClient) { }

  getUsersByMonth(): Observable<number> {
    return this.http.get<number>(`${this.API}/month`).pipe(catchError(this.handleError));
  }

  getUsersByStatus(): Observable<number> {
    return this.http.get<number>(`${this.API}/status`).pipe(catchError(this.handleError));
  }

  getUsersByAdmin(): Observable<number> {
    return this.http.get<number>(`${this.API}/admin`).pipe(catchError(this.handleError));
  }
  
  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.log('Erro de conexão com a API', error.message);
    } else {
      console.log(`API retornou código:${error.status}, mensagem: `, error.message);
    }
    return throwError(() => new Error('Ocorreu um erro durante o processamento da requisição, tente novamente mais tarde!'));
  }
}
