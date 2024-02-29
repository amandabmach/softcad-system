import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, catchError, of, retry, tap, throwError } from 'rxjs';
import { Usuario } from '../../models/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsersRequestService {

  private readonly API = `https://localhost:7032/usuarios`;
  changed = new Subject<void>();


  constructor(private http: HttpClient) { }

  getUser(id: number): Observable<Usuario> {
    return this.http.get<Usuario>(`${this.API}/${id}`)
      .pipe(
        retry(3),
        catchError(this.handleError)
      );
  }

  getUsersByAdmin(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(`${this.API}/administrador`)
      .pipe(
        retry(3),
        catchError(this.handleError)
      );
  }

  createUser(body: any): Observable<Usuario> {
    return this.http.post<Usuario>(this.API, body)
      .pipe(
        tap(() => this.changed.next()),
        catchError(this.handleError)
      );
  }

  deleteUser(idUser: any): Observable<Usuario> {
    return this.http.delete<Usuario>(`${this.API}/${idUser}`)
      .pipe(
        tap(() => this.changed.next()),
        catchError(this.handleError)
      );
  }

  updateUser(body: any): Observable<Usuario> {
    return this.http.put<Usuario>(`${this.API}`, body)
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
