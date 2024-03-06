import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, map, throwError} from 'rxjs';
import { AuthenticatorService } from '../authenticator.service';
import { Administrator } from '../../models/administrator';

@Injectable({
  providedIn: 'root'
})
export class AdminsRequestService {

  private readonly API = `https://localhost:7032/administrators`;

  constructor(
    private http: HttpClient,
    private auth: AuthenticatorService
  ) { }

  login(body: any) : Observable<any> {
    return this.http.post(`${this.API}/login`, body)
    .pipe(
      map(data => this.auth.setToken(data)),
      catchError(this.handleError)
    );
  }

  getAdministrator(): Observable<Administrator> {
    var id = this.auth.getIdAdmin();
    return this.http.get<Administrator>(`${this.API}/${id}`).pipe(catchError(this.handleError));
  }

  getPhotoProfile(): Observable<Blob> {
    var id = this.auth.getIdAdmin();
    return this.http.get(`${this.API}/download/${id}`, { responseType: 'blob' });
  }

  createAdministrator(body: any): Observable<any>{
    return this.http.post<Administrator>(this.API, body)
    .pipe(
      map(data => this.auth.setToken(data)),
      catchError(this.handleError)  
    );
  }

  updateAdministrator(body: any): Observable<Administrator> {
    var id = this.auth.getIdAdmin();
    body = { ...body, id: id };

    const formData: FormData = new FormData();
  
    for (const key in body) {
      if (body.hasOwnProperty(key)) {
        formData.append(key, body[key]);
      }
    }
    return this.http.put<Administrator>(`${this.API}/${id}`, formData).pipe(catchError(this.handleError));
  }
  
  deleteAdministrator(): Observable<Administrator>{
    var id = this.auth.getIdAdmin();
    return this.http.delete<Administrator>(`${this.API}/${id}`).pipe(catchError(this.handleError));
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

