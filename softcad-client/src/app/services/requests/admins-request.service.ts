import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, map, retry, throwError} from 'rxjs';
import { AuthenticatorService } from '../authenticator.service';
import { Administrador } from '../../models/administrador';

@Injectable({
  providedIn: 'root'
})
export class AdminsRequestService {

  private readonly API = `https://localhost:7032/administradores`;

  constructor(
    private http: HttpClient,
    private auth: AuthenticatorService
  ) { }

  login(body: any) : Observable<any> {
    return this.http.post(`${this.API}/login`, body)
    .pipe(
      retry(3),
      map(data => this.auth.setToken(data)),
      catchError(this.handleError)
    );
  }

  getAdministrador(): Observable<Administrador> {
    var id = this.auth.getIdAdmin();
    return this.http.get<Administrador>(`${this.API}/${id}`).pipe(catchError(this.handleError));
  }

  getPhotoProfile(): Observable<Blob> {
    var id = this.auth.getIdAdmin();
    return this.http.get(`${this.API}/download/${id}`, { responseType: 'blob' });
  }

  createAdministrador(body: any): Observable<any>{
    return this.http.post<Administrador>(this.API, body)
    .pipe(
      map(data => this.auth.setToken(data)),
      catchError(this.handleError)  
    );
  }

  updateAdministrador(body: any): Observable<Administrador> {
    var id = this.auth.getIdAdmin();
    body = { ...body, id: id };

    const formData: FormData = new FormData();
  
    for (const key in body) {
      if (body.hasOwnProperty(key)) {
        formData.append(key, body[key]);
      }
    }
    return this.http.put<Administrador>(`${this.API}/${id}`, formData).pipe(catchError(this.handleError));
  }
  
  deleteAdministrador(): Observable<Administrador>{
    var id = this.auth.getIdAdmin();
    return this.http.delete<Administrador>(`${this.API}/${id}`).pipe(catchError(this.handleError));
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

