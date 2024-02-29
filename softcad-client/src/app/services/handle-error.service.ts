import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HandleErrorService {

  constructor() { }
  
  handleError<T>(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.log('Erro de conexão com a API', error.message);
    } else {
      console.log(`API retornou código:${error.status}, mensagem: `, error.message);
    }
    return throwError(() => new Error('Ocorreu um erro durante o processamento da requisição, tente novamente mais tarde!'));
  }
}