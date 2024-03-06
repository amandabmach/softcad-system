import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, catchError, retry, tap, throwError } from 'rxjs';
import { User } from '../../models/user';

@Injectable({
  providedIn: 'root'
})
export class UsersRequestService {

  private readonly API = `https://localhost:7032/users`;
  changed = new Subject<void>();


  constructor(private http: HttpClient) { }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.API}/${id}`)
      .pipe(catchError(this.handleError));
  }

  getUsersByAdmin(): Observable<User[]> {
    return this.http.get<User[]>(`${this.API}/administrator`)
      .pipe(catchError(this.handleError));
  }

  createUser(body: any): Observable<User> {
    return this.http.post<User>(this.API, body)
      .pipe(
        tap(() => this.changed.next()),
        catchError(this.handleError)
      );
  }

  deleteUser(idUser: any): Observable<User> {
    return this.http.delete<User>(`${this.API}/${idUser}`)
      .pipe(
        tap(() => this.changed.next()),
        catchError(this.handleError)
      );
  }

  updateUser(body: any): Observable<User> {
    return this.http.put<User>(`${this.API}`, body)
      .pipe(
        tap(() => this.changed.next()),
        catchError(this.handleError)
      );
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
